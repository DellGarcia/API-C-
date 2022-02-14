using AwesomeApp.Models;
using AwesomeApp.Pages;
using AwesomeApp.Services;
using System;
using System.ComponentModel;
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

                if(_userService.IsUserFieldsValid(user))
                {
                    var result = await _userService.AddUser(user);
                    if (typeof(User).IsInstanceOfType(result))
                        MoveToUserRegisterConfirmationPage(user);
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
    
        private void MoveToUserRegisterConfirmationPage(User user)
        {
            var confirmationVM = new RegisterConfirmationViewModel();
            confirmationVM.Init(user);

            var confirmationPage = new RegisterConfirmationPage
            {
                BindingContext = confirmationVM
            };

            Application.Current.MainPage.Navigation.PushModalAsync(confirmationPage);
        }
    }
}
