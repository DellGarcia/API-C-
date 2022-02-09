using AwesomeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamWebApiClient;

namespace AwesomeApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegisterPage : ContentPage
    {
        public UserRegisterPage()
        {
            InitializeComponent();
            BindingContext =
                Startup.ServiceProvider.GetService<UserRegisterViewModel>();
        }
    }
}