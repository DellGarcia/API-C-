using AwesomeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWebApiClient;

namespace AwesomeApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSavePage : ContentPage
    {
        public UserSavePage()
        {
            InitializeComponent();
            BindingContext =
                Startup.ServiceProvider.GetService<UserSaveViewModel>();
        }
    }
}