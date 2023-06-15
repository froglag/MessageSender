using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Interfaces
{
    /// <summary>
    /// Represents an interface for sending messages to recipients.
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// Sends a message to the specified recipient.
        /// </summary>
        /// <param name="recipient">The recipient's contact information.</param>
        /// <param name="message">The message content to be sent.</param>
        /// <returns>A task representing the asynchronous sending operation.</returns>
        void SendMessage(string recipient, string message);
    }
}
