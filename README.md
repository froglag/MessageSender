Message Sender
==================
This library is designed to make working with the Twilio API easier, especially for sending SMS messages. It provides convenient methods and functions that make it easy to send messages using Twilio.
***
Using this library, developers can easily configure and send SMS messages, minimizing the complexity of interacting with the Twilio API.
***

### Features

* SMS Sending: Ability to send SMS messages through your project.
* Delivery Status Retrieval: Ability to retrieve information about the delivery status of SMS messages.
* Integration with other services: Support for integration with various services and platforms to extend the functionality of your project.
* Ease of use: The project provides a convenient and intuitive interface for managing SMS sending and tracking delivery status.
* Reliability and Security: Reliable delivery of SMS messages and ensuring the security of transmitted data are guaranteed.

### example of usage

```csharp
using MessageSender.Senders;

// Creating an instance of the SmsSender class
var smsSender = new SmsSender();

// Set the credentials for sending SMS
var accountSid = "YOUR_ACCOUNT_SID";
var authToken = "YOUR_AUTH_TOKEN";
var phoneNumber = "YOUR_PHONE_NUMBER";

smsSender.SetSmsCredentials(accountSid, authToken, phoneNumber);

// Sending an SMS message
var recipient = "+1234567890";
var message = "Hi, this is a test SMS!";
smsSender.SendMessage(recipient, message);

// Get delivery status of SMS message
var deliveryStatus = smsSender.GetDeliveryStatus(smsSender.Sid);
```


