using OnlineShop.DTO;
using OnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace OnlineShop.Services
{
    public class TwilioSmsService : ISmsService
    {
        public Task<SmsServiceResponseDTO> SendVerificationCode(string phoneNumber, string code)
        {
            const string accountSid = "AC0c8bc70bfdcfe95d4f1901c6c7e9feed";
            const string authToken = "9aa5ccfca9a6cef64e3b488601221930";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: code,
                from: new Twilio.Types.PhoneNumber("+12192171178"),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );


            return Task.FromResult(new SmsServiceResponseDTO{
                StatusCode = 200,
                Message = "Сообщение отправлено"
            });
        }
    }
}
