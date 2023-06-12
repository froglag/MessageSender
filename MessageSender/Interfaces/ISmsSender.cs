using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Interfaces
{
    /// <summary>
    /// Represents an interface for sending SMS messages.
    /// </summary>
    public interface ISmsSender : IMessageSender
    {
        /// <summary>
        /// Sets the credentials required for sending SMS messages.
        /// </summary>
        /// <param name="accountSid">The account SID for authentication.</param>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="phoneNumber">The phone number associated with the sender.</param>
        void SetSmsCredentials(string accountSid, string authToken, string phoneNumber);
    }
}
