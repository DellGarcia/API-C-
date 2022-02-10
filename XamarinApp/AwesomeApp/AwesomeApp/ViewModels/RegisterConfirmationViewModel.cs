using System;
using System.Collections.Generic;
using System.Text;
using AwesomeApp.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace AwesomeApp.ViewModels
{
    public class SaveUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SaveUserViewModel(User user)
        {
            WelcomeMessage = $"Seja bem vindo(a), {user.FirstName}";
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
