namespace CortanaGameSample.ViewModels
{
    public class KingdomViewModel
    {
        #region Constructors and Destructors

        public KingdomViewModel()
        {
            this.LastAttack = new AttackReportViewModel();
            this.Construction = new ConstructionViewModel();
            this.Protection = new ProtectionViewModel();
            this.Treasury = new TreasuryViewModel();
        }

        #endregion

        #region Properties

        public ConstructionViewModel Construction { get; private set; }

        public AttackReportViewModel LastAttack { get; private set; }

        public ProtectionViewModel Protection { get; private set; }

        public TreasuryViewModel Treasury { get; private set; }

        #endregion
    }
}