using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagerMAUI.Models;
using TaskManagerMAUI.Services;
using TaskManagerMAUI.Views;

namespace TaskManagerMAUI.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private TaskRestService _restService;

        IDisplayMessageAndGoBack _view;

        public ICommand OpenDetail { get; private set; }
        public ICommand CreateTask { get; private set; }

        public ObservableCollection<TaskViewModel> Tasks { get; private set; }

        public TaskListViewModel(TaskRestService taskRestService)
        {
            _restService = taskRestService;

            Tasks = new ObservableCollection<TaskViewModel>();
            OpenDetail = new Command<TaskViewModel>(OnOpenDetail);
            CreateTask = new Command(OnCreate);

            InitializeTasksListsAsync();
        }

        private async void InitializeTasksListsAsync()
        {
            List<UserTask> userTasks = await GetTasksAsync();
        
            foreach(var task in userTasks)
            {
                Tasks.Add(new TaskViewModel(task, _restService));
            }
            OnPropertyChanged(nameof(Tasks));
        }

        private async Task<List<UserTask>> GetTasksAsync()
        {
            List<UserTask> tasks = await _restService.GetUsersTasksAsync(Constants.CurrentUser.Id);

            return tasks;
        }

        private void OnOpenDetail(TaskViewModel taskViewModel)
        {
            IDictionary<string, object> navigationParameter = new Dictionary<string, object>
            {
                {"TaskViewModel", taskViewModel}
            };
            Shell.Current.GoToAsync(nameof(TaskView), navigationParameter);
        }

        private void OnCreate()
        {
            TaskViewModel taskViewModel = new TaskViewModel(_restService);

            IDictionary<string, object> navigationParameter = new Dictionary<string, object>
            {
                {"TaskViewModel", taskViewModel}
            };
            Shell.Current.GoToAsync(nameof(TaskView), navigationParameter);
        }
    }
}
