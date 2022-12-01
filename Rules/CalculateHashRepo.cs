using Microsoft.Extensions.Options;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TradeArtTest.Interfaces;

namespace TradeArtTest.Data
{
  public class CalculateHashRepo : ICalculateHashRepo
  {
    private readonly Settings _settings;

    public CalculateHashRepo(IOptions<Settings> settings)
    {
      _settings = settings.Value;
    }

    public Task<string> CalculateHash()
    {
      string filePath = _settings.filePath;

      using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        return Task.FromResult(HashFile(fs));
    }

    public string HashFile(FileStream stream)
    {
      StringBuilder sb = new StringBuilder();

      if (stream != null)
      {
        stream.Seek(0, SeekOrigin.Begin);

        MD5 md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(stream);
        foreach (byte b in hash)
          sb.Append(b.ToString("x2"));

        stream.Seek(0, SeekOrigin.Begin);
      }

      return sb.ToString();
    }
  }
}
