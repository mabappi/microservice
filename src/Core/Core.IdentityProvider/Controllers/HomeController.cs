using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
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
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public HomeController([NotNull]ILogger<HomeController> logger, IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider)
        {
            _logger = logger;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsAuthenticated())
            {
                return new OkObjectResult(User.Identity.Name);
            }

            var context = await _interaction.GetAuthorizationContextAsync("/");
            
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var props = new AuthenticationProperties {
                    RedirectUri = Url.Action(nameof(Callback)),
                    Items =
                    {
                        { "returnUrl", "/" },
                        { "scheme", context.IdP },
                    }
                };

                return Challenge(props, context.IdP);

            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Callback()
        {
            return Ok("Called");
        }

    }
}
