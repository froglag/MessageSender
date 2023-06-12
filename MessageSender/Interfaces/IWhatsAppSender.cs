using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Interfaces
{
    /// <summary>
    /// Interface for sending WhatsApp messages.
    /// </summary>
    public interface IWhatsAppSender : IMessageSender
    {
        /// <summary>
        /// Sets the credentials required for sending WhatsApp messages.
        /// </summary>
        /// <param name="accountSid">The account SID for authentication.</param>
        /// <param name="authToken">The authentication token.</param>
        void SetWhatsAppCredentials(string accountSid, string authToken);
    }
}
