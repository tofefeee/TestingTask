using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeArtTest.Interfaces;
using TradeArtTest.Models;
using TradeArtTest.Rules;
using static TradeArtTest.Models.PageAssets;

namespace TradeArtTest.Data
{
  public class FetchAssestRepo : IFetchAssetsRepo
  {
    private static Settings _settings;
    private static HttpClient _httpClient;

    public FetchAssestRepo(IOptions<Settings> settings)
    {
      _settings = settings.Value;
      _httpClient = new HttpClient();
    }

    public async Task<List<Price>> FetchAssetsAsync()
    {
      Query query = new Query(Res.PageAssetsQuery, null, Res.PageAssetsQueryName);
      string jsonString = JsonConvert.SerializeObject(query);

      string result = await PostRequest(_settings.blocktapUrl, jsonString);
      PageAssets assets = JsonConvert.DeserializeObject<PageAssets>(result);
      List<Price> prices = new List<Price>();
      List<Task<List<Price>>> runningTasks = new List<Task<List<Price>>>();

      for (int i = 0; i < _settings.assetsCount; i+= _settings.priceBatchSize)
        runningTasks.Add(GetPricesAsync(assets.data.assets[i..(i + _settings.priceBatchSize)]));

      while (runningTasks.Count > 0)
      {
        Task<List<Price>> finishedTask = await Task.WhenAny(runningTasks);
        prices.AddRange(finishedTask.Result);
        runningTasks.Remove(finishedTask);
      }

      return await Task.FromResult(prices);
    }

    async Task<List<Price>> GetPricesAsync(Asset[] assets)
    {
      List<Price> prices = new List<Price>();
      List<Task<string>> runningTasks = new List<Task<string>>();

      for (int i = 0; i < assets.Length; i++)
      {
        Query query = new Query(
         string.Format(Res.PriceQuery, assets[i].assetSymbol, Res.quoteSymbol, Res.exchangeSymbol),
         null,
         Res.PriceQueryName);

        string jsonString = JsonConvert.SerializeObject(query);

        runningTasks.Add(PostRequest(_settings.blocktapUrl, jsonString));
      }

      while (runningTasks.Count > 0)
      {
        Task<string> finishedTask = await Task.WhenAny(runningTasks);
        prices.Add(JsonConvert.DeserializeObject<Price>(finishedTask.Result));
        runningTasks.Remove(finishedTask);
      }

      return await Task.FromResult(prices);
    }

    private async Task<string> PostRequest(string url, string requestString)
    {
      StringContent content = new StringContent(requestString, Encoding.UTF8, _settings.mediaType);
      content.Headers.ContentLength = requestString.Length;

      var response = _httpClient.PostAsync(url, content);
      string result = await response.Result.Content.ReadAsStringAsync();

      return await Task.FromResult(result);
    }
  }
}
