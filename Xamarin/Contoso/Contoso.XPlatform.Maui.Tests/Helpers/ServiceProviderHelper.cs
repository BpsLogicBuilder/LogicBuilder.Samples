using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Maui.Tests.Mocks;
using Contoso.XPlatform.Services;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Maui.Tests.Helpers
{
    internal static class ServiceProviderHelper
    {
        private static readonly IServiceProvider serviceProvider;

        static ServiceProviderHelper()
        {
            MauiProgram.Init(s => { });

            //Replace may interfere with other tests
            //App.ServiceCollection.Replace(new ServiceDescriptor(typeof(IHttpService), typeof(HttpServiceMock), ServiceLifetime.Singleton));
            //App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
            //serviceProvider = App.ServiceProvider;
            ServiceCollection services = new();
            HashSet<Type> types = new();
            foreach (var service in App.ServiceCollection)
            {
                if (service.ServiceType == typeof(IHttpService))
                    services.Add(new ServiceDescriptor(service.ServiceType, typeof(HttpServiceMock), service.Lifetime));
                else if (service.ServiceType == typeof(IAppLogger))
                    services.Add(new ServiceDescriptor(service.ServiceType, typeof(AppLoggerMock), service.Lifetime));
                else if (service.ServiceType == typeof(IRulesCache))
                    services.Add(new ServiceDescriptor(service.ServiceType, typeof(RulesCacheMock), service.Lifetime));
                else
                    services.Add(service);

                types.Add(service.ServiceType);
            }

            if (!types.Contains(typeof(IAppLogger)))
                services.Add(new ServiceDescriptor(typeof(IAppLogger), typeof(AppLoggerMock), ServiceLifetime.Singleton));

            if (!types.Contains(typeof(IRulesCache)))
                services.Add(new ServiceDescriptor(typeof(IRulesCache), typeof(RulesCacheMock), ServiceLifetime.Singleton));

            if (!types.Contains(typeof(IUpdateOnlyFieldsCollectionBuilder)))
                services.Add(new ServiceDescriptor(typeof(IUpdateOnlyFieldsCollectionBuilder), typeof(UpdateOnlyFieldsCollectionBuilder), ServiceLifetime.Singleton));

            serviceProvider = services.BuildServiceProvider();
            App.ServiceProvider = serviceProvider;
        }

        internal static IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
