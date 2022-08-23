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

            List<Task> tasks = new List<Task>
            {
                (delay = Task.Delay(400)),
                (getResponse = action())
            };

            bool activityIndicatorRunning = false;

            while (tasks.Any())
            {
                Task finishedTask = await Task.WhenAny(tasks);
                if (object.ReferenceEquals(finishedTask, delay) && response == null)
                    ShowBusyIndaicator();
                else if (object.ReferenceEquals(finishedTask, getResponse))
                    response = getResponse.Result;

                tasks.Remove(finishedTask);
            }

            RemoveBusyIndaicator();

            return response ?? throw new ArgumentException($"{nameof(response)}: {{189D9682-88D9-440C-BEEB-7B77E345FCA3}}");

            void ShowBusyIndaicator()
            {
                activityIndicatorRunning = true;
                MainThread.BeginInvokeOnMainThread
                (
                    () => App.Current!.MainPage!.Navigation.PushModalAsync/*App.Current.MainPage is not null at this point*/
                    (
                        new Views.BusyIndicator(), false
                    )
                );
            }

            void RemoveBusyIndaicator()
            {
                if (!activityIndicatorRunning)
                    return;

                MainThread.BeginInvokeOnMainThread
                (
                    () => App.Current!.MainPage!.Navigation.PopModalAsync(false)/*App.Current.MainPage is not null at this point*/
                );
            }
        }
    }
}
