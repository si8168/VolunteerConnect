using SQLite;
using VolunteerConnect.Models;

namespace VolunteerConnect.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _db;

        public async Task InitialiseAsync()
        {
            if (_db != null) return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "volunteerconnect.db3");
            _db = new SQLiteAsyncConnection(dbPath);

            await _db.CreateTableAsync<VolunteerOpportunity>();
            await _db.CreateTableAsync<VolunteerRegistration>();

            int count = await _db.Table<VolunteerOpportunity>().CountAsync();
            if (count == 0)
            {
                var sampleOpportunities = new List<VolunteerOpportunity>
                {
                    new VolunteerOpportunity
                    {
                        Title = "Community Garden Helper",
                        Category = "Environment",
                        Date = "Every Saturday",
                        Time = "09:00 AM - 12:00 PM",
                        Location = "Auckland Domain",
                        Description = "Assist with weeding, planting seasonal vegetables, and maintaining garden paths.",
                        Requirements = "Enthusiasm for outdoor work; gloves provided.",
                        AvailablePlaces = 5,
                        ImageName = "dotnet_bot.png",
                        IsAvailable = true
                    },
                    new VolunteerOpportunity
                    {
                        Title = "Food Bank Packing Assistant",
                        Category = "Community Support",
                        Date = "Wednesdays & Fridays",
                        Time = "01:00 PM - 04:00 PM",
                        Location = "Manukau Community Centre",
                        Description = "Pack non-perishable food parcels for local families in need.",
                        Requirements = "Ability to lift up to 10kg safely.",
                        AvailablePlaces = 3,
                        ImageName = "dotnet_bot.png",
                        IsAvailable = true
                    },
                    new VolunteerOpportunity
                    {
                        Title = "Library Support Volunteer",
                        Category = "Education",
                        Date = "Tuesdays",
                        Time = "03:30 PM - 05:30 PM",
                        Location = "Central City Library",
                        Description = "Help organise children's reading activities and re-shelf books.",
                        Requirements = "Friendly demeanor; standard reference check.",
                        AvailablePlaces = 2,
                        ImageName = "dotnet_bot.png",
                        IsAvailable = true
                    },
                    new VolunteerOpportunity
                    {
                        Title = "Beach Clean-up Volunteer",
                        Category = "Environment",
                        Date = "First Sunday of Month",
                        Time = "10:00 AM - 01:00 PM",
                        Location = "Mission Bay",
                        Description = "Join our community team clearing plastic pollution and coastal litter.",
                        Requirements = "All ages welcome. Bring hat and sunscreen.",
                        AvailablePlaces = 15,
                        ImageName = "dotnet_bot.png",
                        IsAvailable = true
                    },
                    new VolunteerOpportunity
                    {
                        Title = "Digital Skills Support Volunteer",
                        Category = "Education",
                        Date = "Thursdays",
                        Time = "10:00 AM - 12:00 PM",
                        Location = "Papakura Community Hub",
                        Description = "Assist seniors with smartphone, tablet, and email basics.",
                        Requirements = "Patience and clear communication skills.",
                        AvailablePlaces = 4,
                        ImageName = "dotnet_bot.png",
                        IsAvailable = true
                    }
                };

                await _db.InsertAllAsync(sampleOpportunities);
            }
        }

        public async Task<List<VolunteerOpportunity>> GetOpportunitiesAsync()
        {
            await InitialiseAsync();
            return await _db!.Table<VolunteerOpportunity>().ToListAsync();
        }

        public async Task<VolunteerOpportunity?> GetOpportunityAsync(int id)
        {
            await InitialiseAsync();
            return await _db!.Table<VolunteerOpportunity>().Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<VolunteerRegistration>> GetRegistrationsAsync()
        {
            await InitialiseAsync();
            return await _db!.Table<VolunteerRegistration>().ToListAsync();
        }

        public async Task<VolunteerRegistration?> GetRegistrationAsync(int id)
        {
            await InitialiseAsync();
            return await _db!.Table<VolunteerRegistration>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddRegistrationAsync(VolunteerRegistration registration)
        {
            await InitialiseAsync();
            return await _db!.InsertAsync(registration);
        }

        public async Task<int> UpdateRegistrationAsync(VolunteerRegistration registration)
        {
            await InitialiseAsync();
            return await _db!.UpdateAsync(registration);
        }

        public async Task<int> DeleteRegistrationAsync(VolunteerRegistration registration)
        {
            await InitialiseAsync();
            return await _db!.DeleteAsync(registration);
        }
    }
}