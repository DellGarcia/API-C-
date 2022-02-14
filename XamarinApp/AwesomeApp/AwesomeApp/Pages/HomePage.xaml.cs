using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWebApiClient;

using AwesomeApp.ViewModels;

namespace AwesomeApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            //BindingContext =
            //    Startup.ServiceProvider.GetService<HomeViewModel>();
        }
    }
}