using System.Collections.Generic;

namespace WBH.Livescoring.Frontend.Entities;

public class Match : IEntity<long>
{
    #region NavigationProperties

    public virtual ICollection<Score> Scores { get; set; }
    public virtual ICollection<Status> Status { get; set; }

    #endregion

    #region IEntity

    public long Id { get; set; }

    #endregion

    #region Properties

    public string Court { get; set; }
    public string TournamentName { get; set; }
    public long? TournamentId { get; set; }

    public string Team1Line1 { get; set; }
    public string Team1Line2 { get; set; }
    public string Team2Line1 { get; set; }
    public string Team2Line2 { get; set; }

    public string MatchTime { get; set; }
    public HomeAway Service { get; set; }

    #endregion
}