using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageSender.Senders;
using Moq;
using Twilio.Exceptions;
using MessageSender.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MessageSender.Tests
{
    [TestClass]
    public class SmsSenderTest
    {
        public IConfigurationRoot config;

        [TestInitialize]
        public void TestInit()
        {
            string appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(appSettingsPath, false, true);
            config = builder.Build();
        }
        
        [TestMethod]
        public async Task SendMessage_SuccessfullySendsMessage()
        {
            string accountSid = config["Twilio:AccountSid"];
            string authToken = config["Twilio:AuthToken"];
            string phoneNumber = config["Twilio:FromPhoneNumber"];
            string recipient = config["Twilio:Recipient"];
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
            string accountSid = config["Twilio:AccountSid"];
            string authToken = config["Twilio:AuthToken"];
            string phoneNumber = config["Twilio:FromPhoneNumber"];
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
            string accountSid = config["Twilio:AccountSid"];
            string authToken = config["Twilio:AuthToken"];
            string phoneNumber = "";
            string recipient = config["Twilio:Recipient"];
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
            string authToken = config["Twilio:AuthToken"];
            string phoneNumber = config["Twilio:FromPhoneNumber"];
            string recipient = config["Twilio:Recipient"];
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<TwilioException>(() => smsSender.SendMessage(recipient, message));
        }

        [TestMethod]
        public void SendMessage_WrongAuthorizationToken()
        {
            string accountSid = config["Twilio:AccountSid"];
            string authToken = "";
            string phoneNumber = config["Twilio:FromPhoneNumber"];
            string recipient = config["Twilio:Recipient"];
            string message = "Test message to my phone number";

            var smsSender = new SmsSender();
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<TwilioException>(() => smsSender.SendMessage(recipient, message));
        }
        
        [TestMethod]
        public void SendMessage_WrongMessageLength()
        {
            string accountSid = config["Twilio:AccountSid"];
            string authToken = config["Twilio:AuthToken"];
            string phoneNumber = config["Twilio:FromPhoneNumber"];
            string recipient = config["Twilio:Recipient"];
            string message = "1234567891011";

            var smsSender = new SmsSender();
            smsSender.MaxMessageLength = 10;
            smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

            Assert.ThrowsException<InvalidOperationException>(() => smsSender.SendMessage(recipient, message));
        }

        [TestMethod]
        public void SendMessage_EmptyMessage()
        {
            string accountSid = config["Twilio:AccountSid"];
            string authToken = config["Twilio:AuthToken"];
            string phoneNumber = config["Twilio:FromPhoneNumber"];
            string recipient = config["Twilio:Recipient"];
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

            string recipient = config["Twilio:Recipient"];
            string message = "Test message to my phone number";

            // Setup the mock to throw an exception when SendMessage is called
            smsSenderMock.Setup(s => s.SendMessage(recipient, message))
                .Throws(new ApiConnectionException("Network error occurred."));

            ApiConnectionException exception = Assert.ThrowsException<ApiConnectionException>(() => smsSender.SendMessage(recipient, message));

            Assert.AreEqual("Network error occurred.", exception.Message);
        }
    }
}
