using MessageSender.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Types;

namespace MessageSender.Senders
{
    // TODO: Add support for sending messages via WhatsApp.
    // Implement the WhatsAppSender class that inherits from WhatsAppSenderBase
    // and implements methods for sending messages via the WhatsApp API.
    public class WhatsAppSender : WhatsAppSenderBase
    {
        // TODO: Perform initialization in the SetWhatsAppCredentials method of the WhatsAppSender class.
        /// <summary>
        /// Sets the credentials required for sending WhatsApp messages.
        /// </summary>
        /// <param name="accountSid">The account SID for authentication.</param>
        /// <param name="authToken">The authentication token.</param>
        public override void SetWhatsAppCredentials(string accountSid, string authToken)
        {
            
        }

        // TODO: Implement sending a message via the WhatsApp API in the SendMessage method of the WhatsAppSender class.
        /// <summary>
        /// Sends a WhatsApp message asynchronously.
        /// </summary>
        /// <param name="recipient">The recipient's contact information.</param>
        /// <param name="message">The message content to be sent.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task SendMessage(string recipient, string message)
        {
            try
            {
                // TODO: Implement the logic to send a message through the WhatsApp API.
                // You can use the Twilio SDK or any other appropriate API or library.

                Console.WriteLine($"Sending WhatsApp message to {recipient}: {message}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
