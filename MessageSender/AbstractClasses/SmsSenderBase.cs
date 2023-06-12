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


        public event ExceptionEventHandler ExceptionOccurred;
        public abstract Task SendMessage(string recipient, string message);

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

        protected void InitializeTwilioClient()
        {
            TwilioClient.Init(AccountSid, AuthToken);
        }

        protected virtual void HandleException(Exception ex)
        {
            ExceptionOccurred?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }
}
