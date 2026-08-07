using VolunteerConnect.Services;
using Xunit;

namespace VolunteerConnect.Tests
{
    public class PrivacyValidatorTests
    {
        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user.name@domain.co.nz", true)]
        [InlineData("plainaddress", false)]
        [InlineData("@missinguser.com", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValidEmail_ValidatesCorrectly(string email, bool expected)
        {
            // Act
            bool result = PrivacyValidator.IsValidEmail(email);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0211234567", true)]
        [InlineData("+64211234567", true)]
        [InlineData("03-345-6789", true)]
        [InlineData("123", false)] // Too short
        [InlineData("abc-def-ghij", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValidPhone_ValidatesCorrectly(string phone, bool expected)
        {
            // Act
            bool result = PrivacyValidator.IsValidPhone(phone);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("alex@test.nz", true)]
        [InlineData("0219876543", true)]
        [InlineData("not-a-valid-contact", false)]
        public void IsValidContact_AcceptsEmailOrPhone(string contact, bool expected)
        {
            // Act
            bool result = PrivacyValidator.IsValidContact(contact);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}