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

    using CortanaGameSample.IO;
    using CortanaGameSample.Model;

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
                var voiceCommandServiceConnection =
                    VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);
                voiceCommandServiceConnection.VoiceCommandCompleted += this.OnVoiceCommandCompleted;

                var voiceCommand = await voiceCommandServiceConnection.GetVoiceCommandAsync();

                if (voiceCommand.CommandName == "Treasury")
                {
                    this.ShowTreasury(voiceCommandServiceConnection);
                }
                else if (voiceCommand.CommandName == "Attack")
                {
                    this.ShowAttack(voiceCommandServiceConnection);
                }
                else if (voiceCommand.CommandName == "Construction")
                {
                    this.ShowConstruction(voiceCommandServiceConnection);
                }
                else if (voiceCommand.CommandName == "Protection")
                {
                    this.ShowProtection(voiceCommandServiceConnection);
                }
            }
        }

        private async void ShowProtection(VoiceCommandServiceConnection voiceCommandServiceConnection)
        {
            // Show we've recognized the command.
            await this.ReportProgress("Checking your protection status...", voiceCommandServiceConnection);

            // Read data.
            var serializer = new FantasyKingdomSerializer();
            var protection = await serializer.Load<Protection>();

            // Return answer.
            string message;

            if (protection != null)
            {
                if (protection.ExpirationTime < DateTime.Now)
                {
                    message = string.Format(
                        "Unfortunately, your protection has expired at {0}.",
                        protection.ExpirationTime);
                }
                else
                {
                    message = string.Format("Yes, you are still protected until {0}.", protection.ExpirationTime);
                }
            }
            else
            {
                message = "Sorry, I couldn't find any protection status data.";
            }

            await this.ReportSuccess(message, voiceCommandServiceConnection);
        }

        private async void ShowConstruction(VoiceCommandServiceConnection voiceCommandServiceConnection)
        {
            // Show we've recognized the command.
            await this.ReportProgress("Checking your construction progress...", voiceCommandServiceConnection);

            // Read data.
            var serializer = new FantasyKingdomSerializer();
            var construction = await serializer.Load<Construction>();

            // Return answer.
            string message;

            if (construction != null)
            {
                if (construction.FinishedTime < DateTime.Now)
                {
                    message = string.Format(
                        "Your {0} has been finished at {1}.",
                        construction.ConstructionName,
                        construction.FinishedTime);
                }
                else
                {
                    message = string.Format(
                        "Your {0} will be finished at {1}.",
                        construction.ConstructionName,
                        construction.FinishedTime);
                }
            }
            else
            {
                message = "Sorry, I couldn't find any constructions.";
            }

            await this.ReportSuccess(message, voiceCommandServiceConnection);
        }

        private async void ShowAttack(VoiceCommandServiceConnection voiceCommandServiceConnection)
        {
            // Show we've recognized the command.
            await this.ReportProgress("Checking for recent attacks...", voiceCommandServiceConnection);

            // Read data.
            var serializer = new FantasyKingdomSerializer();
            var attackReport = await serializer.Load<AttackReport>();

            // Return answer.
            var message = attackReport != null
                ? string.Format(
                    "You have been attacked at {0} by {1}.",
                    attackReport.AttackTime,
                    attackReport.AttackerName)
                : "There haven't been any attacked recently.";

            await this.ReportSuccess(message, voiceCommandServiceConnection);
        }

        private async void ShowTreasury(VoiceCommandServiceConnection voiceCommandServiceConnection)
        {
            // Show we've recognized the command.
            await this.ReportProgress("Checking your treasury...", voiceCommandServiceConnection);

            // Read data.
            var serializer = new FantasyKingdomSerializer();
            var treasury = await serializer.Load<Treasury>();

            // Return answer.
            var message = treasury != null
                ? string.Format(
                    "You have {0} gold.", treasury.Gold)
                : "Sorry, I couldn't find your treasury.";

            await this.ReportSuccess(message, voiceCommandServiceConnection);            
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

        private async Task ReportProgress(string message, VoiceCommandServiceConnection voiceCommandServiceConnection)
        {
            var response = this.CreateResponse(message);
            await voiceCommandServiceConnection.ReportProgressAsync(response);
        }

        private async Task ReportSuccess(string message, VoiceCommandServiceConnection voiceCommandServiceConnection)
        {
            var response = this.CreateResponse(message);
            await voiceCommandServiceConnection.ReportSuccessAsync(response);
        }

        private VoiceCommandResponse CreateResponse(string message)
        {
            var userProgressMessage = new VoiceCommandUserMessage { DisplayMessage = message, SpokenMessage = message };
            return VoiceCommandResponse.CreateResponse(userProgressMessage);
        }

        #endregion
    }
}