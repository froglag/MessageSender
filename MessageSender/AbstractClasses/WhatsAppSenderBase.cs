using MessageSender.Handlers;
using MessageSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace MessageSender.AbstractClasses
{
    public abstract class WhatsAppSenderBase : IWhatsAppSender
    {
        // TODO: Implement the SendMessage method to send messages via WhatsApp.
        /// <summary>
        /// Sends a message asynchronously via WhatsApp.
        /// </summary>
        /// <param name="recipient">The recipient's contact information.</param>
        /// <param name="message">The message content to be sent.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task SendMessage(string recipient, string message);

        // TODO: Implement the SetWhatsAppCredentials method to set the credentials required for sending WhatsApp messages.
        /// <summary>
        /// Sets the credentials required for sending WhatsApp messages.
        /// </summary>
        /// <param name="accountSid">The account SID for authentication.</param>
        /// <param name="authToken">The authentication token.</param>
        public abstract void SetWhatsAppCredentials(string accountSid, string authToken);

        public event ExceptionEventHandler ExceptionOccurred;

        protected void InitializeTwilioClient()
        {
            // TODO: Perform any necessary initialization for the Twilio client here.
            // Example: TwilioClient.Init(accountSid, authToken);
        }

        protected virtual void HandleException(Exception ex)
        {
            ExceptionOccurred?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }
}
