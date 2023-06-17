using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using Twilio;
using MessageSender.Handlers;

namespace MessageSender.AbstractClasses
{
    /// <summary>
    /// Base abstract class for sending SMS messages.
    /// </summary>
    public abstract class SmsSenderBase : ISmsSender
    {
        protected string AccountSid { get; private set; }
        protected string AuthToken { get; private set; }
        protected string PhoneNumber { get; private set; }

        public int MaxMessageLength { get; set; } = 250;

        public event ExceptionEventHandler ExceptionOccurred;
        public abstract void SendMessage(string recipient, string message);

        /// <summary>
        /// Sets the credentials required for sending SMS messages.
        /// </summary>
        /// <param name="accountSid">The account SID for authentication.</param>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="phoneNumber">The phone number associated with the sender.</param>
        public virtual void SetSmsCredentials(string accountSid, string authToken, string phoneNumber)
        {
            AccountSid = accountSid;
            AuthToken = authToken;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Initializes the Twilio client with the account SID and authentication token.
        /// </summary>
        protected void InitializeTwilioClient()
        {
            TwilioClient.Init(AccountSid, AuthToken);
        }

        /// <summary>
        /// Handles any exception that occurs during message sending.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        protected virtual void HandleException(Exception ex)
        {
            ExceptionOccurred?.Invoke(this, new ExceptionEventArgs(ex));
        }

        /// <summary>
        /// Checks if the message length is valid.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>True if the message length is valid; otherwise, false.</returns>
        protected bool IsMessageValid(string message)
        {
            return message.Length <= MaxMessageLength;
        }

        /// <summary>
        /// Retrieves the delivery status of a sent message.
        /// </summary>
        /// <param name="messageId">The ID of the sent message.</param>
        /// <returns>The delivery status of the message.</returns>
        public abstract DeliveryStatus GetDeliveryStatus(string messageId);
    }
}
