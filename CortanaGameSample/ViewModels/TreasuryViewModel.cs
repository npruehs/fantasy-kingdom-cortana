namespace CortanaGameSample.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class TreasuryViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int gold;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

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