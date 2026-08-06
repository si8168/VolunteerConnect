using Microsoft.Maui.Controls;
using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views
{
    [QueryProperty(nameof(OpportunityId), "id")]
    public partial class OpportunityDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private VolunteerOpportunity? _opportunity;

        public string OpportunityId
        {
            set => LoadOpportunity(int.Parse(value));
        }

        public OpportunityDetailsPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void LoadOpportunity(int id)
        {
            _opportunity = await _databaseService.GetOpportunityAsync(id);
            if (_opportunity != null)
            {
                TitleLabel.Text = _opportunity.Title;
                CategoryLabel.Text = _opportunity.Category;
                LocationLabel.Text = $"📍 Location: {_opportunity.Location}";
                DateLabel.Text = $"📅 Date: {_opportunity.Date}";
                TimeLabel.Text = $"⏰ Time: {_opportunity.Time}";
                PlacesLabel.Text = $"👥 Places Available: {_opportunity.AvailablePlaces}";
                DescriptionLabel.Text = _opportunity.Description;
                RequirementsLabel.Text = _opportunity.Requirements;
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (_opportunity != null)
            {
                await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?oppId={_opportunity.Id}");
            }
        }
    }
}