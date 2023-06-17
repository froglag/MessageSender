using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MessageSender.Senders.SmsSender;

namespace MessageSender.Interfaces
{
    /// <summary>
    /// Interface for retrieving the delivery status of a sent message.
    /// </summary>
    public interface IGetDeliveryStatus
    {
        /// <summary>
        /// Retrieves the delivery status of a sent message.
        /// </summary>
        /// <param name="messageId">The ID of the sent message.</param>
        /// <returns>The delivery status of the message.</returns>
        DeliveryStatus GetDeliveryStatus(string messageId);
    }

    /// <summary>
    /// Enumeration of delivery status options for a message.
    /// </summary>
    public enum DeliveryStatus
    {
        Sent,       // The message has been sent.
        Delivered,  // The message has been successfully delivered.
        Failed,     // The message delivery has failed.
        Unknown     // The delivery status is unknown or not available.
    }
}

