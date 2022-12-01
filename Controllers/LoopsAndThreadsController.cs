using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeArtTest.Interfaces;

namespace TradeArtTest.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LoopsAndThreadsController : ControllerBase
  {
    private readonly ILoopsAndThreadsRepo _repository;
    public LoopsAndThreadsController(ILoopsAndThreadsRepo repo)
    {
      _repository = repo;
    }

    //GET api/LoopsAndThreads
    [HttpGet]
    public async Task<ActionResult<string>> LoopsAndThreadsAsync()
    {
      var result = await _repository.DoLoop();
      return Ok(result);
    }
  }
}
