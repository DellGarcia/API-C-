using AwesomeApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using XamWebApiClient;

namespace AwesomeApp.Pages
{
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();
            BindingContext = 
                Startup.ServiceProvider.GetService<UserListViewModel>();
        }
    }
}