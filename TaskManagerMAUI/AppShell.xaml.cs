using TaskManagerMAUI.Views;

namespace TaskManagerMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EnterView), typeof(EnterView));
            Routing.RegisterRoute(nameof(RegistrationView), typeof(RegistrationView));
            Routing.RegisterRoute(nameof(TasksList), typeof(TasksList));
            Routing.RegisterRoute(nameof(TaskView), typeof(TaskView));
        }
    }
}