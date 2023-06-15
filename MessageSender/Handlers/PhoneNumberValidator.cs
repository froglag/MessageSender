using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Twilio.Types;

namespace MessageSender.Handlers
{
    public static class PhoneNumberValidator
    {
        /// <summary>
        /// Validates the format of a phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>True if the phone number is in the correct format, false otherwise.</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            string pattern = @"^\+\d{1,15}$";

            Regex regex = new Regex(pattern);

            bool isMatch = regex.IsMatch(phoneNumber);

            bool isDigitsOnly = phoneNumber.Skip(1).All(char.IsDigit);

            return isMatch && isDigitsOnly;
        }

        public static bool IsValidRecipientPhoneNumber(string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
            {
                return false;
            }

            string pattern = @"^\+420\d{9}$";

            Regex regex = new Regex(pattern);

            bool isMatch = regex.IsMatch(recipient);

            bool isDigitsOnly = recipient.Skip(1).All(char.IsDigit);

            return isMatch && isDigitsOnly;
        }
    }
}
