using Microsoft.AspNetCore.Mvc;
using WBH.Livescoring.Frontend.Logic.ScoreBoard;

namespace WBH.Livescoring.Frontend.API.Controller;

/// <summary>
/// Controller für die Spielstandsanzeige
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ScoreBoardController : ControllerBase
{
    #region Fields

    private readonly IScoreBoardProvider _provider;

    #endregion

    #region Constructors

    public ScoreBoardController(IScoreBoardProvider provider)
    {
        _provider = provider;
    }

    #endregion
    
    #region Endpoints

    /// <summary>
    ///     Test Funktion
    /// </summary>
    /// <remarks>Testet die API</remarks>
    /// <returns>Test</returns>
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Test");
    }

    #endregion
}