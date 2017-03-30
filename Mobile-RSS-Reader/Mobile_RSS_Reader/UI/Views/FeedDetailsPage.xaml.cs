using Mobile_RSS_Reader.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_RSS_Reader.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedDetailsPage : ContentPage
    {
        private FeedDetailsViewModel _model;

        public FeedDetailsPage(FeedDetailsViewModel model)
        {
            _model = model;
            InitializeComponent();
            BindingContext = _model;
        }
    }
}
