using TaskManagerMAUI.Services;
using TaskManagerMAUI.Views;

namespace TaskManagerMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonRegistration_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegistrationView));
        }

        private async void ButtonEnter_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(EnterView));
        }
    }
}