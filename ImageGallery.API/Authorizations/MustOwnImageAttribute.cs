using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorizations
{
    public class MustOwnImageAttribute : AuthorizeAttribute, IAuthorizationRequirementData
    {
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            return [new MustOwnImageRequirement()];
        }
    }
}
