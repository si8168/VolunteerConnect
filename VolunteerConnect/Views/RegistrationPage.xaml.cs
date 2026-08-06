using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views
{
    [QueryProperty(nameof(OpportunityId), "oppId")]
    public partial class RegistrationPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private VolunteerOpportunity? _opportunity;

        public string OpportunityId
        {
            set => LoadOpportunity(int.Parse(value));
        }

        public RegistrationPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void LoadOpportunity(int id)
        {
            _opportunity = await _databaseService.GetOpportunityAsync(id);
            if (_opportunity != null)
            {
                OpportunityTitleLabel.Text = $"Register for: {_opportunity.Title}";
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (_opportunity == null) return;

            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                ShowError("Please enter your preferred name.");
                return;
            }

            if (!PrivacyValidator.IsValidContact(ContactEntry.Text))
            {
                ShowError("Please enter a valid email address or phone number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(AvailabilityEntry.Text))
            {
                ShowError("Please enter your availability.");
                return;
            }

            if (!ConsentCheckBox.IsChecked)
            {
                ShowError("You must agree to the privacy consent to register.");
                return;
            }

            var reg = new VolunteerRegistration
            {
                OpportunityId = _opportunity.Id,
                OpportunityTitle = _opportunity.Title,
                PreferredName = NameEntry.Text.Trim(),
                ContactDetail = ContactEntry.Text.Trim(),
                Availability = AvailabilityEntry.Text.Trim(),
                Notes = NotesEditor.Text?.Trim() ?? string.Empty,
                ConsentGiven = true,
                RegistrationDate = DateTime.Now
            };

            await _databaseService.AddRegistrationAsync(reg);
            await DisplayAlert("Success", "Your registration has been saved successfully!", "OK");
            await Shell.Current.GoToAsync("//MyRegistrationsPage");
        }

        private void ShowError(string msg)
        {
            MessageLabel.TextColor = Colors.Red;
            MessageLabel.Text = msg;
        }
    }
}