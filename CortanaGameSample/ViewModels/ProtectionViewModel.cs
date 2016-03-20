namespace CortanaGameSample.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class ProtectionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private DateTime expirationTime;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

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