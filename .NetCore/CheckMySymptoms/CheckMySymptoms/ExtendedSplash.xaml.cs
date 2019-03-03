using AutoMapper;
using CheckMySymptoms.AutoMapperProfiles;
using CheckMySymptoms.Domain;
using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Flow.Rules;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Repositories;
using CheckMySymptoms.Utils;
using CheckMySymptoms.ViewModels;
using CheckMySymptoms.WindowsStore;
using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CheckMySymptoms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page, INotifyPropertyChanged
    {
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();
            this.DataContext = this;

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);
            splash = splashscreen;

            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();

                // Optional: Add a progress ring to your splash screen to show users that content is loading
                PositionRing();
            }

            // Create a Frame to act as the navigation context
            rootFrame = new Frame();

            // Restore the saved session state if necessary
            RestoreState(loadState);
        }

        #region Fields
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Fields

        #region Properties
        private double _progress;

        public double Progress
        {
            get { return _progress; }
            set
            {
                if (_progress == value)
                    return;

                _progress = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Progress"));
            }
        }

        #endregion Properties

        #region Methods
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;
        }

        private void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }

        void RestoreState(bool loadState)
        {
            if (loadState)
            {
                // TODO: write code to load state
            }
        }

        async Task SetProgressValue(double val)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                Progress = val;
                //splashProgressRing.Value = val;
            });
        }

        async void DismissExtendedSplash()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                //rootFrame = new Frame();
                rootFrame.Content = new MainPage();
                Window.Current.Content = rootFrame;
            });
        }

        async Task<IServiceProvider> GetServiceProvider(ServiceCollection services)
        {
            await SetProgressValue(1);
            services.AddSingleton<AutoMapper.IConfigurationProvider>
            (
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfiles(typeof(BaseClassMappings).Assembly);
                    cfg.AllowNullCollections = true;
                })
            )
            .AddSingleton<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
            .AddSingleton<IRulesLoader, RulesLoader>()
            //.AddSingleton<ILookUpsRepository, LookUpsRepository>()
            .AddSingleton<FlowDataCache, FlowDataCache>()
            .AddSingleton<IVariableMetadataRepository, VariableMetadataRepository>()
            .AddSingleton<IPatientDataRepository, PatientDataRepository>()
            .AddSingleton<IFlowManager, FlowManager>()
            .AddSingleton<UiNotificationService, UiNotificationService>()
            .AddSingleton<FlowActivityFactory, FlowActivityFactory>()
            .AddSingleton<DirectorFactory, DirectorFactory>()
            .AddSingleton<ScreenData, ScreenData>()
            .AddSingleton<ICustomActions, CustomActions>()
            .AddSingleton<ICustomDialogs, CustomDialogs>()
            .AddSingleton<IFlowStateRepository, FlowStateRepository>()
            .AddSingleton<IRulesCache>(sp =>
            {
                //return Task.Run(async () => await LoadRules(sp.GetRequiredService<IRulesLoader>())).Result;//Does not work
                return GetRulesCache(sp.GetRequiredService<IRulesLoader>());
            });

            return await Task.Run(() => services.BuildServiceProvider());
        }

        RulesCache GetRulesCache(IRulesLoader rulesLoader)
        {
            return Task.Run(async () => await LoadRules(rulesLoader)).Result;
        }

        async Task<RulesCache> LoadRules(IRulesLoader rulesLoader)
        {
            RulesCache cache = new RulesCache(new Dictionary<string, RuleEngine>(), new Dictionary<string, string>());
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder rulesFolder = await appInstalledFolder.GetFolderAsync("Rules");
            IReadOnlyList<StorageFile> files = await rulesFolder.GetFilesAsync();

            Dictionary<string, StorageFile> rules = files.Where(f => f.Name.EndsWith(".module")).ToDictionary(f => Path.GetFileNameWithoutExtension(f.Name).ToLowerInvariant());
            Dictionary<string, StorageFile> resources = files.Where(f => f.Name.EndsWith(".resources")).ToDictionary(f => Path.GetFileNameWithoutExtension(f.Name).ToLowerInvariant());

            int count = 0;
            foreach (string key in rules.Keys)
            {
                count++;
                await SetProgressValue(((double)count / rules.Count) * 100);
                rulesLoader.LoadRules(new RulesModuleModel { Name = key, ResourceSetFile = GetBytes(resources[key]), RuleSetFile = GetBytes(rules[key]) }, cache);
            }

            return cache;
        }

        private static byte[] GetBytes(StorageFile file)
        {
            using (IInputStream stream = Task.Run(async () => await file.OpenSequentialReadAsync()).Result)
            {
                using (Stream readStream = stream.AsStreamForRead())
                {
                    byte[] byteArray = new byte[readStream.Length];
                    readStream.Read(byteArray, 0, byteArray.Length);
                    return byteArray;
                }
            }
        }
        #endregion Methods

        #region Event Handlers
        private void ExtendedSplash_OnResize(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (splash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();
                PositionRing();
            }
        }

        private async void DismissedEventHandler(SplashScreen sender, object args)
        {
            dismissed = true;

            (Application.Current as App).ServiceProvider = await GetServiceProvider(new ServiceCollection());


            IRulesCache cache = (Application.Current as App).ServiceProvider.GetRequiredService<IRulesCache>();

            UiNotificationService uiNotificationService = (Application.Current as App).ServiceProvider.GetRequiredService<UiNotificationService>();
            GetSelectedVoice(uiNotificationService, ApplicationData.Current.RoamingSettings);
            uiNotificationService.HasAdFree = await StoreAccessHelper.Instance.CheckIfUserHasAdFreeSubscriptionAsync();
            DismissExtendedSplash();
        }

        private void GetSelectedVoice(UiNotificationService uiNotificationService, ApplicationDataContainer roamingSettings)
        {
            Dictionary<string, VoiceModel> voices = Helpers.SupportedVoices.ToDictionary(v => v.Id);

            if (roamingSettings.Values[RoamingData.SelectedVoice] is string selectedVoice && voices.TryGetValue(selectedVoice, out VoiceModel voice))
                uiNotificationService.SelectedVoice = selectedVoice;
            else
                uiNotificationService.SelectedVoice = voices.Count == 0 ? null : voices.First().Key;

            if (roamingSettings.Values[RoamingData.VoiceOn] is bool voiceOn)
                uiNotificationService.IsVoiceOn = voiceOn;
            else
                uiNotificationService.IsVoiceOn = false;
        }
        #endregion Event Handlers
    }
}
