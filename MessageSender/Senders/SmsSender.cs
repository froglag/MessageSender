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
using System.Reflection.Metadata.Ecma335;
using MessageSender.Interfaces;
using Serilog;
using Serilog.Sinks;

namespace MessageSender.Senders
{
    /// <summary>
    /// SMS sender implementation using Twilio.
    /// </summary>
    public class SmsSender : SmsSenderBase
    {

        public string Sid { get; set; }

        private ILogger logger;

        public SmsSender()
        {
            // Инициализация логгера
            logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();
        }

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
                throw new Exception($"An error occurred while setting SMS credentials: {ex.Message}");
            }
        }

        /// <summary>
        /// Sends an SMS message.
        /// </summary>
        /// <param name="recipient">The recipient's contact information.</param>
        /// <param name="message">The message content to be sent.</param>
        public override void SendMessage(string recipient, string message)
        {

            if (!IsMessageValid(message))
            {
                logger.Error("Message exceeds the maximum length. Message not sent.");
                throw new InvalidOperationException("Message exceeds the maximum length.");
            }

            if (!PhoneNumberValidator.IsValidRecipientPhoneNumber(recipient))
            {
                logger.Error("Invalid recipient number format. Message not sent.");
                throw new FormatException("Invalid recipient number format.");
            }

            if (!PhoneNumberValidator.IsValidPhoneNumber(PhoneNumber))
            {
                logger.Error("Invalid phone number format. Message not sent.");
                throw new FormatException("Invalid phone number format.");
            }

            if (string.IsNullOrEmpty(message))
            {
                logger.Error("Empty message. Message not sent.");
                throw new ArgumentException("Empty message.");
            }

            try
            {
                // Send an SMS message using Twilio

                var messageOptions = new CreateMessageOptions(new PhoneNumber(recipient));
                messageOptions.From = new PhoneNumber(PhoneNumber);
                messageOptions.Body = message;

                var smsMessage = MessageResource.Create(messageOptions);
                GetMessageSid(smsMessage.Sid);

                logger.Information($"SMS sent successfully to number: {smsMessage.To}!");
            }
            catch (Twilio.Exceptions.AuthenticationException ex)
            {
                logger.Error(ex, "Network error occurred. Message not sent.");
                throw new Twilio.Exceptions.AuthenticationException($"Error details: {ex.Message}");
            }
            catch (Twilio.Exceptions.ApiConnectionException ex)
            {
                logger.Error(ex, "Network error occurred. Message not sent.");
                throw new Twilio.Exceptions.ApiConnectionException($"Error details: {ex.Message}");
            }
            catch (Twilio.Exceptions.TwilioException ex)
            {
                logger.Error(ex, "Error occurred while sending the message. Message not sent.");
                throw new Twilio.Exceptions.TwilioException($"Error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during message sending
                logger.Error(ex, "An error occurred while sending the message. Message not sent.");
                HandleException(ex);
            }
        }
        public void GetMessageSid(string sid)
        {
            Sid = sid;
        }

        /// <summary>
        /// Retrieves the delivery status of a sent SMS message.
        /// </summary>
        /// <param name="sid">The SID of the sent message.</param>
        /// <returns>The delivery status of the message.</returns>
        public override DeliveryStatus GetDeliveryStatus(string sid)
        {
            try
            {
                var message = MessageResource.Fetch(sid);

                if (message.Status == MessageResource.StatusEnum.Sent)
                {
                    return DeliveryStatus.Sent;
                }
                else if (message.Status == MessageResource.StatusEnum.Delivered)
                {
                    return DeliveryStatus.Delivered;
                }
                else if (message.Status == MessageResource.StatusEnum.Failed)
                {
                    return DeliveryStatus.Failed;
                }
                else
                {
                    return DeliveryStatus.Unknown;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occurred while fetching message status: {ex.Message}");
                throw;
            }
        }

    }
}
