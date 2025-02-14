using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TZZ.Common.Shared.Interfaces;
using TZZ.Domain.Entities.TheGameZone;

namespace TGZ.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController(IDatabaseService _dbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult Home()
    {
        var gi = _dbContext.Set<Game>().ToList();
        return Ok(gi);
    }


    [HttpGet("[action]")]
    [Authorize(Policy = "default")]
    public ActionResult Restricted()
    {
        return Ok();
    }
}