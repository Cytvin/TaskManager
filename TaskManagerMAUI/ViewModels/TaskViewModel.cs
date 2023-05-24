using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagerMAUI.Models;
using TaskManagerMAUI.Services;
using Microsoft.Maui.Graphics;
using TaskManagerMAUI.Views;

namespace TaskManagerMAUI.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private UserTask _task;
        private TaskRestService _restService;
        private bool _isViewMode;
        private IDisplayMessageAndGoBack _view;

        public ICommand Done { get; private set; }
        public ICommand Create { get; private set; }

        public string Name
        {
            get 
            {
                return _task.Name;
            }
            set 
            {
                _task.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get
            {
                return _task.Description;
            }
            set
            {
                _task.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Location
        {
            get
            {
                return _task.Location;
            }
            set
            {
                _task.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public string Status
        {
            get
            {
                if (_task.IsDone)
                    return "Выполнена";
                else
                    return "Не выполнена";
            }
        }

        public bool ShowDoneButton => !_task.IsDone;
        public bool IsViewMode => _isViewMode;
        public bool IsCreateMode => !_isViewMode;

        public Color BackgroundColor
        {
            get
            {
                if (_task.IsDone)
                    return Colors.Green;
                else
                    return Colors.White;
            }
        }

        public Color TextColor
        {
            get
            {
                if (_task.IsDone)
                    return Colors.White;
                else
                    return Colors.Black;
            }
        }

        public TaskViewModel(UserTask task, TaskRestService taskRestService)
        {
            _isViewMode = true;
            _task = task;
            _restService = taskRestService;

            Done = new Command(OnDone);
        }

        public TaskViewModel(TaskRestService taskRestService)
        {
            _isViewMode = false;
            _task = new UserTask();
            _restService = taskRestService;

            Create = new Command(OnCreate);
        }

        private async void OnDone()
        {
            _task.IsDone = true;
            await _restService.UpdateUserTaskAsync(_task);
        }

        private async void OnCreate()
        {
            try
            {
                await _restService.SaveUserTaskAsync(_task);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка", ex.Message, "ОK");
                return;
            }

            await Shell.Current.DisplayAlert("Готово", "Задача успешно сохранена", "ОК");
            await Shell.Current.GoToAsync("..");
        }
    }
}
