using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.Authorizations
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationPolicy CanAddImage()
        {
            return new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireClaim("country","USA")
                    .RequireClaim("role","Premium")
                    .Build();
        }
    }
}
