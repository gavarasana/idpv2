using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ImageGallery.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfigurationManager _configurationManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IHttpClientFactory httpClientFactory, IConfigurationManager configurationManager, ILogger<AuthenticationController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configurationManager = configurationManager;
            _logger = logger;
        }

        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public async Task Logout()
        {
            var httpClient = _httpClientFactory.CreateClient("IDPClient");
            var discoveryDocumentResponse = await httpClient.GetDiscoveryDocumentAsync();

            if (discoveryDocumentResponse.IsError) {
                throw new Exception(discoveryDocumentResponse.Error);
            }

            //  _logger.LogInformation("Client Id:" + $"{ _configurationManager["IdentityProvider:ClientId"] }");
            //  _logger.LogInformation("Client Secret:" + $"{ _configurationManager["IdentityProvider:ClientSecret"] }");

            var revokeTokenResponse = await httpClient.RevokeTokenAsync(new TokenRevocationRequest {
                Address = discoveryDocumentResponse.RevocationEndpoint,
                ClientId = "ImageGalleryClient", //  _configurationManager["IdentityProvider:ClientId"],
                ClientSecret = "hobronlane", // _configurationManager["IdentityProvider:ClientSecret"],
                Token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
            });


            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
