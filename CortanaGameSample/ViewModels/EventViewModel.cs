// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventViewModel.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CortanaGameSample
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class EventViewModel: INotifyPropertyChanged
    {
        private string eventName;

        private DateTime expirationTime;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string EventName
        {
            get
            {
                return this.eventName;
            }
            set
            {
                this.eventName = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime ExpirationTime
        {
            get
            {
                return this.expirationTime;
            }
            set
            {
                this.expirationTime = value;
                this.OnPropertyChanged();
            }
        }
    }
}