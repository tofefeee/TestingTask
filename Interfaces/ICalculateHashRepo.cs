using System.Threading.Tasks;

namespace TradeArtTest.Interfaces
{
  public interface ICalculateHashRepo
  {
    Task<string> CalculateHash();
  }
}
