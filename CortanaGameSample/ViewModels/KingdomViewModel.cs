// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KingdomViewModel.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CortanaGameSample
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;
    using CortanaGameSample.Model;

    public class KingdomViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int currentGold;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public Event CurrentEvent { get; set; }

        public int CurrentGold
        {
            get
            {
                return this.currentGold;
            }
            set
            {
                this.currentGold = value;
                this.OnPropertyChanged();
            }
        }

        public AttackReport LastAttack { get; set; }

        public DateTime ProtectionExpirationTime { get; set; }

        public Construction TownHall { get; set; }

        #endregion

        #region Public Methods and Operators

        public void CollectGold()
        {
            this.CurrentGold += 100;
        }

        public void InitWithRandomValues()
        {
            this.CurrentGold = 300;
            this.TownHall = new Construction
            {
                ConstructionName = "Town Hall",
                FinishedTime = DateTime.Now + TimeSpan.FromHours(1)
            };

            this.LastAttack = new AttackReport
            {
                AttackerName = "EvilPlayer356",
                AttackTime = DateTime.Now - TimeSpan.FromHours(3)
            };

            this.ProtectionExpirationTime = DateTime.Now + TimeSpan.FromHours(2);

            this.CurrentEvent = new Event
            {
                EventName = "Gold Rush",
                ExpirationTime = DateTime.Now + TimeSpan.FromMinutes(37)
            };
        }

        #endregion

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}