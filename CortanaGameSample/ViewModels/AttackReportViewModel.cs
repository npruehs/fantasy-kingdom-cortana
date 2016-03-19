// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttackReportViewModel.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CortanaGameSample
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class AttackReportViewModel : INotifyPropertyChanged
    {
        private string attackerName;

        private DateTime attackTime;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string AttackerName
        {
            get
            {
                return this.attackerName;
            }
            set
            {
                this.attackerName = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime AttackTime
        {
            get
            {
                return this.attackTime;
            }
            set
            {
                this.attackTime = value;
                this.OnPropertyChanged();
            }
        }
    }
}