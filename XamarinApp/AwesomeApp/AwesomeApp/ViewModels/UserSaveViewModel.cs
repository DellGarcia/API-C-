using System;
using Xamarin.Forms;
using System.ComponentModel;
using AwesomeApp.Services;
using AwesomeApp.Models;

namespace AwesomeApp.ViewModels
{
    public class UserSaveViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private User _user;
        public event PropertyChangedEventHandler PropertyChanged;


        public Command SaveUser { get; }

        public UserSaveViewModel(IUserService userService)
        {
            _userService = userService;

            SaveUser = new Command(async () =>
            {
                User updatedUser = new User()
                {
                    Id = _user.Id,
                    FirstName = _firstName,
                    SurName = _surname,
                    Age = _age
                };
                if (_userService.IsUserFieldsValid(updatedUser))
                    await userService.SaveUser(updatedUser);
            });
        }

        public void Init(User user)
        {
            _user = user;
            FindUser();
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

        private async void FindUser()
        {
            _user = await _userService.GetUser((Guid) _user.Id);

            FirstName = _user.FirstName;
            Surname = _user.SurName;
            Age = _user.Age;
        }

        private void MoveToUserSaveConfirmationPage(User user)
        {
            //var confirmationVM = new RegisterConfirmationViewModel(user);

            //var confirmationPage = new SaveConfirmationPage
            //{
            //    BindingContext = confirmationVM
            //};

            //Application.Current.MainPage.Navigation.PushModalAsync(confirmationPage);
        }

    }
}
