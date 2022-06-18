using System;

namespace WBH.Livescoring.Frontend.Entities;

public class Status : IEntity<long>
{
    #region IEntity

    public long Id { get; set; }

    #endregion

    #region Properties

    public long? MatchId { get; set; }
    public string Message { get; set; }
    public DateTime Happened { get; set; }

    #endregion

    #region NavigationProperties

    public virtual Match MatchNavigation { get; set; }

    #endregion
}