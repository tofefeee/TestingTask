using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeArtTest.Data;
using TradeArtTest.Interfaces;
using TradeArtTest.Models;

namespace TradeArtTest.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FetchAssetsController : ControllerBase
  {
    private readonly IFetchAssetsRepo _repository;
    public FetchAssetsController(IFetchAssetsRepo repo)
    {
      _repository = repo;
    }

    //GET api/FetchAssets
    [HttpGet]
    public async Task<ActionResult<Price>> FetchAssets()
    {
      var result = await _repository.FetchAssetsAsync();
      return Ok(result);
    }
  }
}