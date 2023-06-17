using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageSender.Senders;
using Moq;
using Twilio.Exceptions;
using MessageSender.Interfaces;

namespace MessageSender.Tests
{
    [TestClass]
    public class SmsSenderTest
    {
        [TestMethod]
        public async Task SendMessage_SuccessfullySendsMessage()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            smsSender.SendMessage(recipient, message);

            await Task.Delay(TimeSpan.FromSeconds(5));

            var deliveryStatus = smsSender.GetDeliveryStatus(smsSender.Sid);
            Assert.IsTrue(deliveryStatus == DeliveryStatus.Sent || deliveryStatus == DeliveryStatus.Delivered);
        }

        [TestMethod]
        public void SendMessage_WrongRecipientNumber()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            var ex = Assert.ThrowsException<FormatException>(() => smsSender.SendMessage(recipient, message));
            Assert.AreEqual("Invalid recipient number format.", ex.Message);
        }
        
        [TestMethod]
        public void SendMessage_WrongPhoneNumber()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            var ex = Assert.ThrowsException<FormatException>(() => smsSender.SendMessage(recipient, message));
            Assert.AreEqual("Invalid phone number format.", ex.Message);
        }

        [TestMethod]
        public void SendMessage_WrongAccountSid()
        {
            string accountSid = "";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<TwilioException>(() => smsSender.SendMessage(recipient, message));
        }

        [TestMethod]
        public void SendMessage_WrongAuthorizationToken()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<TwilioException>(() => smsSender.SendMessage(recipient, message));
        }
        
        [TestMethod]
        public void SendMessage_WrongMessageLength()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "1234567891011";

            var smsSender = new SmsSender();
            smsSender.MaxMessageLength = 10;
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<InvalidOperationException>(() => smsSender.SendMessage(recipient, message));
        }

        [TestMethod]
        public void SendMessage_EmptyMessage()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<ArgumentException>(() => smsSender.SendMessage(recipient, message));
        }

        [TestMethod]
        public void SendMessage_NetworkError()
        {
            var smsSenderMock = new Mock<SmsSender>();
            var smsSender = smsSenderMock.Object;

            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            // Setup the mock to throw an exception when SendMessage is called
            smsSenderMock.Setup(s => s.SendMessage(recipient, message))
                .Throws(new ApiConnectionException("Network error occurred."));

            ApiConnectionException exception = Assert.ThrowsException<ApiConnectionException>(() => smsSender.SendMessage(recipient, message));

            Assert.AreEqual("Network error occurred.", exception.Message);

<<<<<<< HEAD
        [TestMethod]
        public void SendMessage_WrongPhoneNumberAndWrongAuthToken()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "invalid_auth_token";
            string phoneNumber = "";
            string recipient = "REPLACED_PHONE_NUMBER";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            var ex = Assert.ThrowsException<AggregateException>(() => smsSender.SendMessage(recipient, message));

            // Проверяем, что в исключении содержится исключение формата номера телефона
            Assert.IsTrue(ex.InnerExceptions.Any(e => e is FormatException && e.Message == "Invalid phone number format."));

            // Проверяем, что в исключении содержится исключение аутентификации
            Assert.IsTrue(ex.InnerExceptions.Any(e => e is AuthenticationException && e.Message.StartsWith("Error details")));
=======
            // Verify that no additional actions were performed after the exception
            smsSenderMock.Verify(s => s.SendMessage(recipient, message), Times.Once);
>>>>>>> 30858ae (Logger added to SmsSender.cs)
        }
    }
}
