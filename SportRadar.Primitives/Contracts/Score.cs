namespace WBH.Livescoring.SportRadar
{
    public class Score
    {
        #region Properties

        public string Type { get; set; }

        public double Team1 { get; set; }

        public double Team2 { get; set; }

        public Score SubScore { get; set; }

        #endregion
    }
}