namespace WBH.Livescoring.SportRadar.Types
{
    public interface Score
    {
        public string Type { get;  }

        public double Team1 { get; }

        public double Team2 { get; }

        public Score SubScore { get; }
    }
}