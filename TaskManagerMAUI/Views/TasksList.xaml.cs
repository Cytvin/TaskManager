using TaskManagerMAUI.Services;
using TaskManagerMAUI.ViewModels;

namespace TaskManagerMAUI.Views;

public partial class TasksList : ContentPage, IDisplayMessageAndGoBack
{
	public TasksList(TaskRestService _taskRestService)
	{
		InitializeComponent();

		BindingContext = new TaskListViewModel(_taskRestService);
	}

    public void GoBack()
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string message)
    {
        throw new NotImplementedException();
    }
}