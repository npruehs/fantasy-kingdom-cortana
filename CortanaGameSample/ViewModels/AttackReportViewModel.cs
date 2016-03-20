namespace CortanaGameSample.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class AttackReportViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string attackerName;

        private DateTime attackTime;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

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