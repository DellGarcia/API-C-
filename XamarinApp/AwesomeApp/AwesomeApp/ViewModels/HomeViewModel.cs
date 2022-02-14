using System;
using Xamarin.Forms;
using XamWebApiClient;
using Microsoft.Extensions.DependencyInjection;
using AwesomeApp.Pages;
using System.ComponentModel;

namespace AwesomeApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public Command OpenRegisterPage { get; }
        public Command OpenListPage { get; }

        public HomeViewModel()
        {
            OpenRegisterPage = new Command(MoveToRegisterUserPage);
            OpenListPage = new Command(() =>
            {
                MoveToListUserPage();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MoveToRegisterUserPage()
        {
            var registerUserVM = Startup.ServiceProvider.GetService<UserRegisterViewModel>();

            var registerPage = new UserRegisterPage
            {
                BindingContext = registerUserVM
            };

            Application.Current.MainPage.Navigation.PushAsync(registerPage);
        }

        private void MoveToListUserPage()
        {
            var listUserVM = Startup.ServiceProvider.GetService<UserListViewModel>();

            var listPage = new UserListPage
            {
                BindingContext = listUserVM
            };

            Application.Current.MainPage.Navigation.PushAsync(listPage);
        }
    }
}
