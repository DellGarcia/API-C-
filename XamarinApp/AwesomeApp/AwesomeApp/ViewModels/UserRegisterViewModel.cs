using AwesomeApp.Models;
using AwesomeApp.Pages;
using AwesomeApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AwesomeApp.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IUserService _userService;

        public Command SaveUser { get; }

        public UserRegisterViewModel(IUserService userService)
        {
            _userService = userService;

            SaveUser = new Command(async () =>
            {
                User user = new User()
                {
                    FirstName = _firstName,
                    SurName = _surname,
                    Age = _age
                };

                if(IsUserFieldsValid(user))
                {
                    var result = await _userService.AddUser(user);
                    if (typeof(User).IsInstanceOfType(result))
                        MoveToUserRegisterConfirmationPage(user);
                    else
                        Console.WriteLine("Deu ruim");
                }
            });
        }

        #region Binding Variables

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }

        private string _surname;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        private int? _age;

        public int? Age
        {
            get => _age;
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
            }
        }

        #endregion
    
        private bool IsUserFieldsValid(User user)
        {
            if (user.FirstName == string.Empty || user.FirstName == null)
                return false;

            if (user.Age < 13 || user.Age > 200 || user.Age == null)
                return false;

            return true;
        }
    
        private void MoveToUserRegisterConfirmationPage(User user)
        {
            var confirmationVM = new RegisterConfirmationViewModel(user);

            var confirmationPage = new RegisterConfirmationPage
            {
                BindingContext = confirmationVM
            };

            Application.Current.MainPage.Navigation.PushModalAsync(confirmationPage);
        }
    }
}
