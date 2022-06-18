namespace WBH.Livescoring.Frontend.Entities;

public class Match : IEntity<long>
{
    #region IEntity

    public long Id { get; set; }

    #endregion

    #region Properties

    public string Name { get; set; }

    #endregion
}