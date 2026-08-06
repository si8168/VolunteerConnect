using System.Text.RegularExpressions;

namespace VolunteerConnect.Services
{
    public static class PrivacyValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email.Trim(), pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            string pattern = @"^\+?[0-9\s\-\(\)]{7,15}$";
            return Regex.IsMatch(phone.Trim(), pattern);
        }

        public static bool IsValidContact(string contact)
        {
            return IsValidEmail(contact) || IsValidPhone(contact);
        }
    }
}