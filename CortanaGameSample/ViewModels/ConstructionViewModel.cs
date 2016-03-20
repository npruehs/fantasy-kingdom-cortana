namespace CortanaGameSample.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;

    public class ConstructionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string constructionName;

        private DateTime finishedTime;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

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