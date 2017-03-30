using Mobile_RSS_Reader.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_RSS_Reader.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly HomePageViewModel _model;

        public HomePage(HomePageViewModel model)
        {
            _model = model;
            InitializeComponent();

            BindingContext = _model;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView) sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await _model.HandleItemSelectedAsync(e.SelectedItem as HomePageViewModel.FeedPresentationModel);

            //Deselect Item
            ((ListView) sender).SelectedItem = null;
        }
    }
}

