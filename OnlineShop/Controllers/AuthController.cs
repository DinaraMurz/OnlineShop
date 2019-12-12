using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services;

namespace OnlineShop.Web.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;
        private readonly TwilioSmsService twilioSmsService;

        public AuthController(UserService userService, TwilioSmsService twilioSmsService)
        {
            this.userService = userService;
            this.twilioSmsService = twilioSmsService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string phoneNumber, string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await userService.Authenticate(phoneNumber, code);

            if (String.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpGet]
        public async Task<IActionResult> SendCode(string phoneNumber)
        {
            var code = new Random().Next(100000, 999999).ToString();

            await twilioSmsService.SendVerificationCode(phoneNumber, code);

            await userService.SaveUserCode(phoneNumber, code);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmCode(string phoneNumber, string code)
        {
            if(await userService.ConfirmUserCode(phoneNumber, code))
            {
                return Ok();
            }

            return StatusCode(400);  //
        }

    }
}