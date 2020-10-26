using IdentityServer4.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.IdentityProvider.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController([NotNull]ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([CanBeNull]string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            if (User.IsAuthenticated())
            {
                return new OkObjectResult(User.Identity.Name);
            }

            var props = new AuthenticationProperties {
                RedirectUri = Url.Action(nameof(Callback)),
                Items = {
                    {"returnUrl", returnUrl},
                    {"scheme", Constants.AuthenticationSchemeName},
                }
            };

            return Challenge(props, Constants.AuthenticationSchemeName);
        }

        [HttpGet]
        public IActionResult Callback()
        {
            return Ok($"Called: {User.Identity.Name}");
        }
    }
}
