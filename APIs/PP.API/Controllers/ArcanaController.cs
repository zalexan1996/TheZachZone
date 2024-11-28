using Microsoft.AspNetCore.Mvc;
using TZZ.Core.Shared;

namespace PP.API.Controllers;

[ApiController]
[Route("arcana")]
public class ArcanaController : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult<ZachZoneCommandResponse>> GetArcana()
    {
        return Ok(ZachZoneCommandResponse.Success());
    }
}
