using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageSender.Senders;
using Moq;
using Twilio.Exceptions;


namespace MessageSender.Tests
{
    [TestClass]
    public class SmsSenderTest
    {
        [TestMethod]
        public void SendMessage_SuccessfullySendsMessage()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            smsSender.SendMessage(recipient, message);
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

            Assert.ThrowsException<FormatException>(() => smsSender.SendMessage(recipient, message));
        }
        
        [TestMethod]
        public void SendMessage_WrongePhoneNumber()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<FormatException>(() => smsSender.SendMessage(recipient, message));
        }

        [TestMethod]
        public void SendMessage_WrongeAccountSid()
        {
            string accountSid = "";
            string authToken = "REPLACED_AUTH_TOKEN";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            smsSender.SendMessage(recipient, message);
        }

        [TestMethod]
        public void SendMessage_WrongeAuthorizationToken()
        {
            string accountSid = "REPLACED_ACCOUNT_SID";
            string authToken = "";
            string phoneNumber = "REPLACED_PHONE_NUMBER";
            string recipient = "REPLACED_RECIPIENT";
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            smsSender.SendMessage(recipient, message);
        }
        
        [TestMethod]
        public void SendMessage_WrongeMessageLength()
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

            smsSenderMock.Setup(s => s.SendMessage(recipient, message))
             .Throws(new ApiConnectionException("Network error occurred."));

            ApiConnectionException exception = Assert.ThrowsException<ApiConnectionException>(() => smsSender.SendMessage(recipient, message));

            Assert.AreEqual("Network error occurred.", exception.Message);
        }
    }
}
