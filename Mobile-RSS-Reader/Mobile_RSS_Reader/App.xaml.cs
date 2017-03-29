using System.Threading;
using Mobile_RSS_Reader.Actions;
using Mobile_RSS_Reader.Converters;
using Mobile_RSS_Reader.Data;
using Mobile_RSS_Reader.Parsers;
using Mobile_RSS_Reader.UI.Styles;
using Mobile_RSS_Reader.UI.ViewModels;
using Mobile_RSS_Reader.UI.Views;
using Xamarin.Forms;

namespace Mobile_RSS_Reader
{

    public partial class App : Application
    {
        private readonly string StorageFileName = "FeedReader";

        public App()
        {
            InitializeComponent();

            if (Resources == null)
            {
                Resources = new ResourceDictionary();
            }

            FeedCardStyling.Apply(Resources);

            var tuple = StorageFactory.OpenStorage(StorageFileName, 1, CancellationToken.None);

            if (tuple.Item2 != null)
            {
                tuple.Item2.StartInitializationAsync(CancellationToken.None);
                tuple.Item2.CompleteInitialization();
            }

            var reactiveData = new ReactiveData(tuple.Item1);
            
            MainPage = new HomePage(new HomePageViewModel(new ActionService(reactiveData, tuple.Item1, new FeedProvider(new RssParser(new HtmlToTextConverter()))), reactiveData.FeedListObservable));
  
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
