﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
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

            await RemoveBusyIndaicator().ConfigureAwait(true);

            return response ?? throw new ArgumentException($"{nameof(response)}: {{189D9682-88D9-440C-BEEB-7B77E345FCA3}}");

            Task ShowBusyIndaicator()
            {
                activityIndicatorRunning = true;

                return App.Current!.MainPage!.Navigation.PushModalAsync/*App.Current.MainPage is not null at this point*/
                (
                    new Views.BusyIndicator(), false
                );
            }

            async Task RemoveBusyIndaicator()
            {
                if (!activityIndicatorRunning)
                {
                    await Task.CompletedTask;
                    return;
                }

                await App.Current!.MainPage!.Navigation.PopModalAsync(false).ConfigureAwait(true);
            }
        }
    }
}
