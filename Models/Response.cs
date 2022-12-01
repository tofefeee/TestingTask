namespace TradeArtTest.Models
{
  public class PageAssets
  {
    public Data data;

    public class Data
    {
      public Asset[] assets;
    }
    public class Asset
    {
      public string assetName;
      public string assetSymbol;
      public string marketCap;
    }
  }

  public class Price
  {
    public Data data { get; set; }

    public class Data
    {
      public Market[] markets { get; set; }
    }

    public class Market
    {
      public string marketSymbol { get; set; }

      public Ticker ticker { get; set; }
    }

    public class Ticker
    {
      public decimal lastPrice { get; set; }
    }
  }
}