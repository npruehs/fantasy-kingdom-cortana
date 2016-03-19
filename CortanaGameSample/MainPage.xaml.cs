// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

 // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CortanaGameSample
{
    using System;
    using System.Runtime.CompilerServices;

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
            var serializer = new FantasyKingdomSerializer();
            var attackReport = await serializer.Load<AttackReport>();
            var construction = await serializer.Load<Construction>();
            var protection = await serializer.Load<Protection>();
            var treasury = await serializer.Load<Treasury>();

            // Fill view model.
            if (attackReport != null)
            {
                this.viewModel.LastAttack.AttackTime = attackReport.AttackTime;
                this.viewModel.LastAttack.AttackerName = attackReport.AttackerName;
            }
            else
            {
                this.viewModel.LastAttack.AttackTime = DateTime.Now - TimeSpan.FromHours(3);
                this.viewModel.LastAttack.AttackerName = "EvilPlayer356";
            }

            if (construction != null)
            {
                this.viewModel.Construction.ConstructionName = construction.ConstructionName;
                this.viewModel.Construction.FinishedTime = construction.FinishedTime;
            }
            else
            {
                this.viewModel.Construction.ConstructionName = "Town Hall";
                this.viewModel.Construction.FinishedTime = DateTime.Now + TimeSpan.FromHours(1);
            }

            if (protection != null)
            {
                this.viewModel.Protection.ExpirationTime = protection.ExpirationTime;
            }
            else
            {
                this.viewModel.Protection.ExpirationTime = DateTime.Now + TimeSpan.FromHours(2);
            }

            if (treasury != null)
            {
                this.viewModel.Treasury.Gold = treasury.Gold;
            }
            else
            {
                this.viewModel.Treasury.Gold = 300;
            }
        }

        #endregion

        #region Methods

        private void OnCollect(object sender, RoutedEventArgs e)
        {
            this.CollectGold();
            this.Save();
        }

        #endregion


        public void CollectGold()
        {
            this.viewModel.Treasury.Gold += 100;
        }

        private void Save()
        {
            // Convert view model to model.
            var attackReport = new AttackReport
            {
                AttackTime = this.viewModel.LastAttack.AttackTime,
                AttackerName = this.viewModel.LastAttack.AttackerName
            };
            var construction = new Construction
            {
                ConstructionName = this.viewModel.Construction.ConstructionName,
                FinishedTime = this.viewModel.Construction.FinishedTime
            };
            var protection = new Protection { ExpirationTime = this.viewModel.Protection.ExpirationTime };
            var treasury = new Treasury { Gold = this.viewModel.Treasury.Gold };

            // Save data.
            var serializer = new FantasyKingdomSerializer();
            serializer.Save(attackReport);
            serializer.Save(construction);
            serializer.Save(protection);
            serializer.Save(treasury);
        }
    }
}