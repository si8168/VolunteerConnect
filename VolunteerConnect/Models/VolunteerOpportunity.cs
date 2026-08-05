using SQLite;

namespace VolunteerConnect.Models
{
    [Table("VolunteerOpportunities")]
    public class VolunteerOpportunity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public int AvailablePlaces { get; set; }
        public string ImageName { get; set; } = "dotnet_bot.png";
        public bool IsAvailable { get; set; } = true;
    }
}