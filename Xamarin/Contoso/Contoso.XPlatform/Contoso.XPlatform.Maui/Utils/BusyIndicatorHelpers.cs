using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Utils
{
    internal static class BusyIndicatorHelpers
    {
        public async static Task<TResult> ExecuteRequestWithBusyIndicator<TResult>(Func<Task<TResult>> action) where TResult : class
        {
            Task delay;
            Task<TResult> getResponse;
            TResult? response = null;

            List<Task> tasks = new()
            {
                (delay = Task.Delay(400)),
                (getResponse = action())
            };

            bool activityIndicatorRunning = false;
            /*Tak.ConfigureAwait(true) for all tasks seems to proevent issue of
             the Busy Indicator window not closing.*/
            while (tasks.Any())
            {
                Task finishedTask = await Task.WhenAny(tasks).ConfigureAwait(true);
                if (object.ReferenceEquals(finishedTask, delay) && response == null)
                    await ShowBusyIndaicator().ConfigureAwait(true);
                else if (object.ReferenceEquals(finishedTask, getResponse))
                    response = getResponse.Result;

                tasks.Remove(finishedTask);
            }
#if ANDROID
            await Task.Delay(100).ConfigureAwait(true);/*Busy Indicator window not closing.*/
#endif
            await RemoveBusyIndaicator().ConfigureAwait(true);

            return response ?? throw new ArgumentException($"{nameof(response)}: {{189D9682-88D9-440C-BEEB-7B77E345FCA3}}");

            Task ShowBusyIndaicator()
            {
                activityIndicatorRunning = true;

                return MainThread.InvokeOnMainThreadAsync
                (/*App.Current.MainPage is not null at this point*/
                    async () => await App.Current!.MainPage!.Navigation.PushModalAsync
                    (
                        new Views.BusyIndicator(), false
                    ).ConfigureAwait(true)
                );
            }

            Task RemoveBusyIndaicator()
            {
                if (!activityIndicatorRunning)
                {
                    return Task.CompletedTask;
                }

                return MainThread.InvokeOnMainThreadAsync
                (/*App.Current.MainPage is not null at this point*/
                    async () => await App.Current!.MainPage!.Navigation.PopModalAsync(false).ConfigureAwait(true)
                );
            }
        }
    }
}
