using DotNetBcBackend.Controllers;
using DotNetBcBackend.Models;
using DotNetBcBackend.Services;
using System;
using Xunit;

namespace DotNetBcTDD
{
    public class RegisterTest
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;

        [Fact]
        public void UserNameValidTest()
        {
            var accountController = new AccountController<>();
        }
    }
}
