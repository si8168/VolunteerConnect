using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views
{
    public partial class OpportunitiesPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<VolunteerOpportunity> _allOpportunities = new();

        public OpportunitiesPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadOpportunitiesAsync();
        }

        private async Task LoadOpportunitiesAsync()
        {
            _allOpportunities = await _databaseService.GetOpportunitiesAsync();
            ApplyFilter();
        }

        private void OnSearchOrFilterChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string query = OpportunitySearchBar.Text?.Trim().ToLower() ?? string.Empty;
            string selectedCategory = CategoryPicker.SelectedItem?.ToString() ?? "All Categories";

            var filtered = _allOpportunities.Where(o =>
                (string.IsNullOrEmpty(query) || o.Title.ToLower().Contains(query) || o.Location.ToLower().Contains(query)) &&
                (selectedCategory == "All Categories" || o.Category == selectedCategory)
            ).ToList();

            OpportunitiesCollectionView.ItemsSource = filtered;
        }

        private async void OnOpportunitySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is VolunteerOpportunity selected)
            {
                OpportunitiesCollectionView.SelectedItem = null;
                await Shell.Current.GoToAsync($"{nameof(OpportunityDetailsPage)}?id={selected.Id}");
            }
        }
    }
}