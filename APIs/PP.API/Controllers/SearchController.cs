using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.PocketPersona.Search;

namespace PP.API.Controllers;

[ApiController]
[Route("[controller]")]
sealed class SearchController(ISender sender) : ControllerBase
{
  [HttpGet("[action]")]
  public async Task<ActionResult<List<SearchResultDto>>> Search(string term)
  {
    var result = await sender.Send(new SearchQuery(term, null));

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
    }

    return Ok(result.Result);
  }
}
