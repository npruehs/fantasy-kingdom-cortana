// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstructionViewModel.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CortanaGameSample
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class ConstructionViewModel: INotifyPropertyChanged
    {
        private string constructionName;

        private DateTime finishedTime;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ConstructionName
        {
            get
            {
                return this.constructionName;
            }
            set
            {
                this.constructionName = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime FinishedTime
        {
            get
            {
                return this.finishedTime;
            }
            set
            {
                this.finishedTime = value;
                this.OnPropertyChanged();
            }
        }
    }
}