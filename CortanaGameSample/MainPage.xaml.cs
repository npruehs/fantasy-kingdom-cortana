// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

 // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CortanaGameSample
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using CortanaGameSample.IO;
    using CortanaGameSample.Model;

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

            this.DataContext = this.viewModel;

            this.Init();
        }

        private async void Init()
        {
            // Load.
            var treasurySerializer = new TreasurySerializer();
            var treasury = await treasurySerializer.Load();

            if (treasury == null)
            {
                this.InitWithRandomValues();
            }
            else
            {
                this.viewModel.Treasury.Gold = treasury.Gold;
            }
        }

        #endregion

        #region Methods

        private void OnCollect(object sender, RoutedEventArgs e)
        {
            this.CollectGold();

            // Save.
            var treasury = new Treasury { Gold = this.viewModel.Treasury.Gold };

            var treasurySerializer = new TreasurySerializer();
            treasurySerializer.Save(treasury);
        }

        #endregion


        public void CollectGold()
        {
            this.viewModel.Treasury.Gold += 100;
        }

        public void InitWithRandomValues()
        {
            this.viewModel.Treasury.Gold = 300;

            this.viewModel.Construction.ConstructionName = "Town Hall";
            this.viewModel.Construction.FinishedTime = DateTime.Now + TimeSpan.FromHours(1);

            this.viewModel.LastAttack.AttackTime = DateTime.Now - TimeSpan.FromHours(3);
            this.viewModel.LastAttack.AttackerName = "EvilPlayer356";

            this.viewModel.Protection.ExpirationTime = DateTime.Now + TimeSpan.FromHours(2);

            this.viewModel.CurrentEvent.EventName = "Gold Rush";
            this.viewModel.CurrentEvent.ExpirationTime = DateTime.Now + TimeSpan.FromMinutes(37);
        }
    }
}