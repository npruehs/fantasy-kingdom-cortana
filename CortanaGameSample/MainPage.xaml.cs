// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

 // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CortanaGameSample
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    ///   An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields

        private readonly KingdomViewModel viewModel;

        #endregion

        #region Constructors and Destructors

        public MainPage()
        {
            this.InitializeComponent();

            this.viewModel = new KingdomViewModel();
            this.viewModel.InitWithRandomValues();

            this.DataContext = this.viewModel;
        }

        #endregion

        #region Methods

        private void OnCollect(object sender, RoutedEventArgs e)
        {
            this.viewModel.CollectGold();
        }

        #endregion
    }
}