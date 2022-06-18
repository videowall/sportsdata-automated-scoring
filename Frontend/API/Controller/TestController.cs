using Microsoft.AspNetCore.Mvc;

namespace WBH.Livescoring.Frontend.API.Controller;

/// <summary>
///     Test Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
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