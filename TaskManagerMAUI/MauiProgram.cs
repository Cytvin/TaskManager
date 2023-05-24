using Microsoft.Extensions.Logging;
using TaskManagerMAUI.Services;
using TaskManagerMAUI.Views;

namespace TaskManagerMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
            builder.Services.AddSingleton<UserRestService>();
            builder.Services.AddSingleton<TaskRestService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<EnterView>();
            builder.Services.AddTransient<RegistrationView>();
            builder.Services.AddTransient<TasksList>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}