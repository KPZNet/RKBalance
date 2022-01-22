namespace RKBalance.Models
{
    public class SPBalanceRun
    {
        public STEVector InitialRunVector { get; set; } = new STEVector();
        public STEVector TrialWeightVector { get; set; } = new STEVector();
        public STEVector TrialRunVector { get; set; } = new STEVector();
        public STEVector InfluenceVector { get; set; } = new STEVector();
        public STEVector WeightPlacementVector { get; set; } = new STEVector();
    }
}