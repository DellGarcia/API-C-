using System;
using System.Collections.Generic;
using System.Text;
using AwesomeApp.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace AwesomeApp.ViewModels
{
    public class RegisterConfirmationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command ReturnToRoot { get; }

        public RegisterConfirmationViewModel()
        {
            ReturnToRoot = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
                Application.Current.MainPage.Navigation.PopToRootAsync();
            });
        }

        public void Init(User user)
        {
            WelcomeMessage = $"{user.FirstName} é um novo usuário";
        }

        private string _welcomeMessage;

        public string WelcomeMessage {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(WelcomeMessage));
            }
        }
    }
}
