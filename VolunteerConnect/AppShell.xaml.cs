using VolunteerConnect.Views;

namespace VolunteerConnect
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register detail and edit routes for Shell navigation
            Routing.RegisterRoute(nameof(OpportunityDetailsPage), typeof(OpportunityDetailsPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(EditRegistrationPage), typeof(EditRegistrationPage));
        }
    }
}