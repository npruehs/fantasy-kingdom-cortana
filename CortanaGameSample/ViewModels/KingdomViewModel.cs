// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KingdomViewModel.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CortanaGameSample
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using CortanaGameSample.Annotations;
    using CortanaGameSample.Model;

    public class KingdomViewModel
    {
        public AttackReportViewModel LastAttack { get; private set; }

        public ConstructionViewModel Construction { get; private set; }

        public EventViewModel CurrentEvent { get; private set; }

        public ProtectionViewModel Protection { get; private set; }

        public TreasuryViewModel Treasury { get; private set; }

        public KingdomViewModel()
        {
            this.LastAttack = new AttackReportViewModel();
            this.Construction = new ConstructionViewModel();
            this.CurrentEvent = new EventViewModel();
            this.Protection = new ProtectionViewModel();
            this.Treasury = new TreasuryViewModel();
        }
    }
}