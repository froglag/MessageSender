using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageSender.AbstractClasses;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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
            base.SetSmsCredentials(accountSid, authToken, phoneNumber);
            InitializeTwilioClient();
        }

        /// <summary>
        /// Sends an SMS message asynchronously.
        /// </summary>
        /// <param name="recipient">The recipient's contact information.</param>
        /// <param name="message">The message content to be sent.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task SendMessage(string recipient, string message)
        {
            try
            {
                // Send an SMS message using Twilio
                var smsMessage = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(PhoneNumber),
                    to: new Twilio.Types.PhoneNumber(recipient)
                );

                Console.WriteLine($"SMS sent successfully to number: {smsMessage.To}!");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during message sending
                HandleException(ex);
            }
        }

    }
}
