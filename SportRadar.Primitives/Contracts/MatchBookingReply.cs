namespace WBH.Livescoring.SportRadar
{
    public class MatchBookingReply
    {
        #region Properties

        public long? MatchId { get; set; }
        public string Message { get; set; }
        public BookMatchResult Result { get; set; }

        #endregion
    }
}