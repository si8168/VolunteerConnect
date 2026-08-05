using SQLite;

namespace VolunteerConnect.Models
{
    [Table("VolunteerRegistrations")]
    public class VolunteerRegistration
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int OpportunityId { get; set; }
        public string OpportunityTitle { get; set; } = string.Empty;
        public string PreferredName { get; set; } = string.Empty;
        public string ContactDetail { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool ConsentGiven { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}