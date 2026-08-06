using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views
{
    [QueryProperty(nameof(RegistrationId), "regId")]
    public partial class EditRegistrationPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private VolunteerRegistration? _registration;

        public string RegistrationId
        {
            set => LoadRegistration(int.Parse(value));
        }

        public EditRegistrationPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void LoadRegistration(int id)
        {
            _registration = await _databaseService.GetRegistrationAsync(id);
            if (_registration == null)
            {
                await DisplayAlert("Error", "The selected registration could not be found.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            TitleLabel.Text = $"Edit: {_registration.OpportunityTitle}";
            NameEntry.Text = _registration.PreferredName;
            ContactEntry.Text = _registration.ContactDetail;
            AvailabilityEntry.Text = _registration.Availability;
            NotesEditor.Text = _registration.Notes;
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            if (_registration == null) return;

            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                ShowError("Name cannot be empty.");
                return;
            }

            if (!PrivacyValidator.IsValidContact(ContactEntry.Text))
            {
                ShowError("Valid email or phone number required.");
                return;
            }

            _registration.PreferredName = NameEntry.Text.Trim();
            _registration.ContactDetail = ContactEntry.Text.Trim();
            _registration.Availability = AvailabilityEntry.Text.Trim();
            _registration.Notes = NotesEditor.Text?.Trim() ?? string.Empty;

            await _databaseService.UpdateRegistrationAsync(_registration);
            await DisplayAlert("Updated", "Your details have been updated successfully.", "OK");
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_registration == null) return;

            bool confirm = await DisplayAlert("Confirm Cancellation", $"Are you sure you want to cancel your registration for '{_registration.OpportunityTitle}'?", "Yes", "No");
            if (confirm)
            {
                await _databaseService.DeleteRegistrationAsync(_registration);
                await DisplayAlert("Deleted", "Your registration has been removed.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        private void ShowError(string msg)
        {
            MessageLabel.TextColor = Colors.Red;
            MessageLabel.Text = msg;
        }
    }
}