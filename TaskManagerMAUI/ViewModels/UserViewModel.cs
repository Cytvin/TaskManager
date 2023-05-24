using System.Windows.Input;
using TaskManagerMAUI.Models;
using TaskManagerMAUI.Services;
using TaskManagerMAUI.Views;

namespace TaskManagerMAUI.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private User _user;
        private UserRestService _restService;
        private IDisplayMessageAndGoBack _view;

        public ICommand Registration { get; private set; }
        public ICommand Enter { get; private set; }

        public string Login
        {
            get { return _user.Login; }
            set 
            { 
                _user.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Name
        {
            get { return _user.Name; }
            set
            {
                _user.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Location
        {
            get { return _user.Location; }
            set
            {
                _user.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public UserViewModel(UserRestService userRestService, IDisplayMessageAndGoBack view) 
        {
            _user = new User();
            _view = view;

            _restService = userRestService;
            Registration = new Command(OnRegistration);
            Enter = new Command(OnEnter);
        }

        private async void OnRegistration()
        {
            try
            {
                await _restService.SaveUserAsync(_user);
            }
            catch (Exception ex) 
            {
                _view.ShowMessage(ex.Message);
                return;
            }

            _view.ShowMessage("Регистрация успешно выполнена");
            _view.GoBack();
        }

        private async void OnEnter()
        {
            try
            {
                Constants.CurrentUser = await _restService.GetUserAsync(Login, Password);
            }
            catch (Exception ex) 
            {
                _view.ShowMessage(ex.Message);
                return;
            }

            await Shell.Current.GoToAsync(nameof(TasksList));
        }
    }
}
