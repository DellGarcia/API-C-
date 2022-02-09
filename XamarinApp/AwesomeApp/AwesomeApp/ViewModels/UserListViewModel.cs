using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using AwesomeApp.Models;
using AwesomeApp.Services;

namespace AwesomeApp.ViewModels
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IUserService userService;

        public Command SelectedUserChangedCommand { get; }
        public Command DeleteUserCommand { get; }
        public Command SaveUserCommand { get; }

        public UserListViewModel(IUserService userService)
        {
            AllUsers = new ObservableCollection<User>();
            this.userService = userService;

            SelectedUserChangedCommand = new Command(() => {});

            DeleteUserCommand = new Command(async () => {
                await userService.DeleteUser(SelectedUser);
                AllUsers.Remove(SelectedUser);
                SelectedUser = null;
            });

            SaveUserCommand = new Command(() => {
                
            });

            GetUsers();
        }

        public ObservableCollection<User> AllUsers { get; set; }

        private User _selectUser;
        public User SelectedUser
        {
            get => _selectUser;  
            set {
                _selectUser = value;
                
                IsSelected = value != null;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedUser)));
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }

        private void MoveToSaveUserPage(User user)
        {
            //var saveUserVM = new RegisterConfirmationViewModel(user);

            //var savePage = new SaveUserPage
            //{
            //    BindingContext = saveUserVM
            //};

            //Application.Current.MainPage.Navigation.PushModalAsync(savePage);
            Console.WriteLine($"Moving to save page for {user.FirstName}");
        }

        private async void GetUsers()
        {
            IEnumerable<User> users = await userService.GetUsers();
            AllUsers.Clear();
            foreach(User user in users)
                AllUsers.Add(user);
        }
    }
}
