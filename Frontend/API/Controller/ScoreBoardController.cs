using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
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
    /// Match buchen
    /// </summary>
    /// <remarks>Bucht ein Match bei SportRadar</remarks>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Book([FromBody, Required] long matchId)
    {
        try
        {
            _provider.BookMatch(matchId);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
        return Ok();
    }

    /// <summary>
    /// Spielstand abrufen
    /// </summary>
    /// <remarks>Ruft den aktuellen Spielstand ab.</remarks>
    /// <returns>Spielstand</returns>
    [HttpGet, Route("{matchId}")]
    public IActionResult GetScoreBoardInfo([FromRoute, Required] long matchId)
    {
        try
        {
            return Ok(_provider.GetScoreBoardInfo(matchId));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    /// <summary>
    /// Verfügbare Spiele abrufen
    /// </summary>
    /// <returns>Liste der Spiele</returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_provider.GetAvailableMatches());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    #endregion
}