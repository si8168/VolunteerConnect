using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private VolunteerOpportunity? _featuredOpportunity;

        public HomePage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadHomeDataAsync();
        }

        private async Task LoadHomeDataAsync()
        {
            var opportunities = await _databaseService.GetOpportunitiesAsync();
            OpportunityCountLabel.Text = $"{opportunities.Count} Local Opportunities";

            if (opportunities.Count > 0)
            {
                _featuredOpportunity = opportunities[0];
                FeaturedTitleLabel.Text = _featuredOpportunity.Title;
                FeaturedDescLabel.Text = _featuredOpportunity.Description;
                FeaturedLocationLabel.Text = $"📍 {_featuredOpportunity.Location}";
            }
        }

        private async void OnViewFeaturedClicked(object sender, EventArgs e)
        {
            if (_featuredOpportunity != null)
            {
                await Shell.Current.GoToAsync($"{nameof(OpportunityDetailsPage)}?id={_featuredOpportunity.Id}");
            }
        }

        private async void OnBrowseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//OpportunitiesPage");
        }
    }
}