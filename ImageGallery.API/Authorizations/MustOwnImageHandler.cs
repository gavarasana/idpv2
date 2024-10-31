using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorizations
{
    public class MustOwnImageHandler : AuthorizationHandler<MustOwnImageRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGalleryRepository _galleryRepository;

        public MustOwnImageHandler(IHttpContextAccessor httpContextAccessor, IGalleryRepository galleryRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _galleryRepository = galleryRepository;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnImageRequirement requirement)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var imageId = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

            if (!Guid.TryParse(imageId, out var imageIdAsGuid))
            {
                context.Fail();
                return;
            }

            if (userId == null)
            {
                context.Fail();
                return;
            }

            if (!( await _galleryRepository.IsImageOwnerAsync(imageIdAsGuid, userId)))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);


        }
    }
}
