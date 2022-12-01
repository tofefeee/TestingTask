using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeArtTest.Interfaces;

namespace TradeArtTest.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CalculateHashController : ControllerBase
  {
    private readonly ICalculateHashRepo _repository;

    public CalculateHashController(ICalculateHashRepo repo)
    {
      _repository = repo;
    }

    //GET api/CalculateHash
    [HttpGet]
    public async Task<ActionResult<string>> CalculateHash()
    {
      var result = await _repository.CalculateHash();
      return Ok(result);
    }
  }
}
