using System;
using System.Linq.Expressions;

namespace WBH.Livescoring.Frontend.Logic.ScoreBoard;

public class Match
{
    #region Properties

    public long Id { get; set; }
    public string Court { get; set; }
    public string Tournament { get; set; }
    public string Team1Line1 { get; set; }
    public string Team1Line2 { get; set; }
    public string Team2Line1 { get; set; }
    public string Team2Line2 { get; set; }
    public Entities.Status Status { get; set; }

    #endregion

    #region Projections

    internal static Expression<Func<Entities.Match, Match>> Project()
    {
        return x => new Match
        {
            Id = x.Id,
            Court = x.Court,
            Tournament = x.TournamentName,
            Team1Line1 = x.Team1Line1,
            Team1Line2 = x.Team1Line2,
            Team2Line1 = x.Team2Line1,
            Team2Line2 = x.Team2Line2,
            Status = x.Status
        };
    }

    #endregion
}