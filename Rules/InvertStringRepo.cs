using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TradeArtTest.Interfaces;

namespace TradeArtTest.Data
{
  public class InvertStringRepo : IInvertStringRepo
  {
    private readonly Settings _settings;

    public InvertStringRepo(IOptions<Settings> settings)
    {
      _settings = settings.Value;
    }

    public Task<string> InvertString()
    {
      string s = _settings.stringToInvert;
      char[] chars = new char[s.Length];
      int length = s.Length - 1;

      for (int i = 0; i <= length; i++)
        chars[i] = s[length - i];

      return Task.FromResult(new string(chars));
    }
  }
}
