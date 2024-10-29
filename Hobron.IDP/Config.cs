using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Hobron.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource("roles", "User Roles", ["role"]),
            new IdentityResource("country", "Country of the user", ["country"])
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
                new ApiScope("ImageGalleryApi.FullAccess","Full Access to Image Gallery API"),
                new ApiScope("ImageGalleryApi.ReadAccess","Read-only Access to Image Gallery API")
            ];

    public static IEnumerable<ApiResource> ApiResources =>
        [
            new ApiResource("ImageGalleryApi", "Image Gallery API",["role", "country"]){
                Scopes = { "ImageGalleryApi.FullAccess" }
            }
        ];
    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client
                {
                    ClientId = "ImageGalleryClient",
                    ClientName = "ImageGallery Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RedirectUris =
                    {
                        "https://localhost:7184/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:7184/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ImageGalleryApi.FullAccess",
                        "roles",
                        "country"
                    },
                    ClientSecrets =
                    {
                        new Secret("hobronlane".Sha256())
                    },
                    RequireConsent = false
                }
            };
}