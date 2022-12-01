using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeArtTest.Interfaces;

namespace TradeArtTest.Data
{
  public class LoopsAndThreadsRepo : ILoopsAndThreadsRepo
  {
    private readonly Settings _settings;
    public LoopsAndThreadsRepo(IOptions<Settings> settings)
    {
      _settings = settings.Value;
    }

    public async Task<string> DoLoop()
    {
      ObservableCollection<int> myList = new ObservableCollection<int>();

      myList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
          async delegate (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
          {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
              await Task.Delay(100); 
          }
      );

      for (int i = 1; i <= _settings.loopSize; i++)
        myList.Add(i);

      return await Task.FromResult(myList.Count.ToString());
    }
  }
}