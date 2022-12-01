using System.Threading.Tasks;

namespace TradeArtTest.Interfaces
{
  public interface IInvertStringRepo
  {
    Task<string> InvertString();
  }
}
