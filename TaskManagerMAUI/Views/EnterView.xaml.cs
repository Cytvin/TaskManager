using TaskManagerMAUI.Services;
using TaskManagerMAUI.ViewModels;

namespace TaskManagerMAUI.Views;

public partial class EnterView : ContentPage, IDisplayMessageAndGoBack
{
	public EnterView(UserRestService userRestService)
	{
		InitializeComponent();

		BindingContext = new UserViewModel(userRestService, this);
	}

    public void GoBack()
    {
        Shell.Current.GoToAsync("..");
    }

    public void ShowMessage(string message)
    {
        Shell.Current.DisplayAlert("", message, "Œ ");
    }
}