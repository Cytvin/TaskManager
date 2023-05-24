using TaskManagerMAUI.ViewModels;

namespace TaskManagerMAUI.Views;

[QueryProperty(nameof(TaskViewModel), "TaskViewModel")]
public partial class TaskView : ContentPage, IDisplayMessageAndGoBack
{
	public TaskViewModel TaskViewModel 
	{ 
		set
		{
			BindingContext = value;
		}
	}

	public TaskView()
	{
		InitializeComponent();
	}

    public void GoBack()
    {
        Shell.Current.GoToAsync("..");
    }

    public void ShowMessage(string message)
    {
        Shell.Current.DisplayAlert("", message, "Отмена");
    }
}