// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FantasyKingdomCortanaService.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CortanaGameSample.Service
{
    using System;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.AppService;
    using Windows.ApplicationModel.Background;
    using Windows.ApplicationModel.VoiceCommands;

    public sealed class FantasyKingdomCortanaService : IBackgroundTask
    {
        #region Fields

        private BackgroundTaskDeferral serviceDeferral;

        #endregion

        #region Public Methods and Operators

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            this.serviceDeferral = taskInstance.GetDeferral();

            taskInstance.Canceled += this.OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null && triggerDetails.Name == "FantasyKingdomCortanaService")
            {
                var voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);
                voiceServiceConnection.VoiceCommandCompleted += this.OnVoiceCommandCompleted;
                VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                await this.ShowProgressScreen("Checking your treasury...", voiceServiceConnection);

                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "You have 100 gold.";
                userMessage.SpokenMessage = "You have 100 gold.";

                var response = VoiceCommandResponse.CreateResponse(userMessage);
                await voiceServiceConnection.ReportSuccessAsync(response);
            }
        }

        #endregion

        #region Methods

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }

        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }

        private async Task ShowProgressScreen(string message, VoiceCommandServiceConnection voiceServiceConnection)
        {
            var userProgressMessage = new VoiceCommandUserMessage();
            userProgressMessage.DisplayMessage = userProgressMessage.SpokenMessage = message;

            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(userProgressMessage);
            await voiceServiceConnection.ReportProgressAsync(response);
        }

        #endregion
    }
}