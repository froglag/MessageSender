using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageSender.AbstractClasses;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using MessageSender.Handlers;
using Twilio.Types;

namespace MessageSender.Senders
{
    /// <summary>
    /// SMS sender implementation using Twilio.
    /// </summary>
    public class SmsSender : SmsSenderBase
    {
        /// <summary>
        /// Sets the credentials required for sending SMS messages and initializes the Twilio client.
        /// </summary>
        /// <param name="accountSid">The Twilio account SID for authentication.</param>
        /// <param name="authToken">The authentication token for the Twilio account.</param>
        /// <param name="phoneNumber">The phone number associated with the sender.</param>
        public override void SetSmsCredentials(string accountSid, string authToken, string phoneNumber)
        {  
            try
            {
                base.SetSmsCredentials(accountSid, authToken, phoneNumber);
                InitializeTwilioClient();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while setting SMS credentials: {ex.Message}");
                throw;
            }
        }
            
        /// <summary>
        /// Sends an SMS message asynchronously.
        /// </summary>
        /// <param name="recipient">The recipient's contact information.</param>
        /// <param name="message">The message content to be sent.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override void SendMessage(string recipient, string message)
        {

            if (!IsMessageValid(message))
            {
                Console.WriteLine("Message exceeds the maximum length. Message not sent.");
                throw new InvalidOperationException("Message exceeds the maximum length.");
            }

            if (!PhoneNumberValidator.IsValidRecipientPhoneNumber(recipient))
            {
                Console.WriteLine("Invalid recipient number format. Message not sent.");
                throw new FormatException("Invalid recipient number format.");
            }

            if (!PhoneNumberValidator.IsValidPhoneNumber(PhoneNumber))
            {
                Console.WriteLine("Invalid phone number format. Message not sent.");
                throw new FormatException("Invalid phone number format.");
            }

            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Empty message. Message not sent.");
                throw new ArgumentException("Empty message.");
            }

            try
            {
                // Send an SMS message using Twilio

                var messageOptions = new CreateMessageOptions(new PhoneNumber(recipient));
                messageOptions.From = new PhoneNumber(PhoneNumber);
                messageOptions.Body = message;

                var smsMessage = MessageResource.Create(messageOptions);

                Console.WriteLine($"SMS sent successfully to number: {smsMessage.To}!");
            }
            catch (Twilio.Exceptions.ApiConnectionException ex)
            {
                Console.WriteLine("Network error occurred. Message not sent.");
                Console.WriteLine($"Error details: {ex.Message}");
            }
            catch (Twilio.Exceptions.TwilioException ex)
            {
                Console.WriteLine("Error occurred while sending the message. Message not sent.");
                Console.WriteLine($"Error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during message sending
                HandleException(ex);
            }
        }

    }
}
