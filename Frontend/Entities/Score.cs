namespace WBH.Livescoring.Frontend.Entities;

public class Score : IEntity
{
    #region Properties

    public long MatchId { get; set; }
    
    public ScoreType Type { get; set; }

    public double Team1 { get; set; }

    public double Team2 { get; set; }
    

    #endregion

    #region NavigationProperties

    public virtual Match MatchNavigation { get; set; }

    #endregion
}