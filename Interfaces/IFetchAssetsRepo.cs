using System.Collections.Generic;
using System.Threading.Tasks;
using TradeArtTest.Models;

namespace TradeArtTest.Interfaces
{
  public interface IFetchAssetsRepo
  {
    Task<List<Price>> FetchAssetsAsync();
  }
}
