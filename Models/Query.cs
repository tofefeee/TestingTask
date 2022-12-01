
namespace TradeArtTest
{
  public class Query
  {
    public Query(string query, string variables, string operationName)
    {
      this.query = query;
      this.variables = variables;
      this.operationName = operationName;
    }

    public string query;
    public string variables;
    public string operationName;
  }
}
