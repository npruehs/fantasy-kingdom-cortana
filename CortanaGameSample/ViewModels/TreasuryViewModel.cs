// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreasuryViewModel.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CortanaGameSample
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class TreasuryViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int gold;

        public int Gold
        {
            get
            {
                return this.gold;
            }
            set
            {
                this.gold = value;
                this.OnPropertyChanged();
            }
        }
    }
}