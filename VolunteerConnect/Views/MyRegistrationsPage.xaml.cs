using VolunteerConnect.Models;
using VolunteerConnect.Services;

namespace VolunteerConnect.Views
{
    public partial class MyRegistrationsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public MyRegistrationsPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadRegistrationsAsync();
        }

        private async Task LoadRegistrationsAsync()
        {
            var list = await _databaseService.GetRegistrationsAsync();
            RegistrationsCollectionView.ItemsSource = list;
            CountLabel.Text = $"Total: {list.Count}";
        }

        private async void OnRegistrationSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is VolunteerRegistration selected)
            {
                RegistrationsCollectionView.SelectedItem = null;
                await Shell.Current.GoToAsync($"{nameof(EditRegistrationPage)}?regId={selected.Id}");
            }
        }
    }
}