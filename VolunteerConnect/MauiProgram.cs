using VolunteerConnect.Services;
using VolunteerConnect.Views;

namespace VolunteerConnect
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

            // Register Services
            builder.Services.AddSingleton<DatabaseService>();

            // Register Pages
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<OpportunitiesPage>();
            builder.Services.AddTransient<OpportunityDetailsPage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<MyRegistrationsPage>();
            builder.Services.AddTransient<EditRegistrationPage>();
            builder.Services.AddTransient<PrivacyPage>();

            return builder.Build();
        }
    }
}