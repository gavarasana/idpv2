using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorizations
{
    public class MustOwnImageRequirement : IAuthorizationRequirement
    {
        public MustOwnImageRequirement() { 
        
        }
    }
}
