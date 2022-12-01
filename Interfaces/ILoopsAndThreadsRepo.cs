using System.Threading.Tasks;

namespace TradeArtTest.Interfaces
{
  public interface ILoopsAndThreadsRepo
  {
    Task<string> DoLoop();
  }
}
