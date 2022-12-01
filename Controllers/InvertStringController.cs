using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeArtTest.Data;

namespace TradeArtTest.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class InvertStringController : ControllerBase
  {
    private readonly InvertStringRepo _repository;

    public InvertStringController(InvertStringRepo repo)
    {
      _repository = repo;
    }

    //GET api/InvertString
    [HttpGet]
    public async Task<ActionResult<string>> InvertString()
    {
      var result = await _repository.InvertString();
      return Ok(result);
    }
  }
}
