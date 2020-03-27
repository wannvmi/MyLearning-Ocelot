using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Data;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace Demo.IdentityServer
{
    public class CustomProfileService : IProfileService
    {
        private readonly DemoDbContext _context;
        private readonly ILogger<CustomProfileService> _logger;

        public CustomProfileService(DemoDbContext context, ILogger<CustomProfileService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(_logger);

            if (context.RequestedClaimTypes.Any())
            {
                var user = _context.User.Find(context.Subject.GetSubjectId());
                if (user != null)
                {
                    //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Id),  //请求用户的账号，这个可以保证User.Identity.Name有值
                        new Claim(JwtClaimTypes.Name, user.Username),  //请求用户的姓名
                    };
                    context.AddRequestedClaims(claims);
                }
            }

            context.LogIssuedClaims(_logger);

            return Task.CompletedTask;
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            _logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = _context.User.Find(context.Subject.GetSubjectId());
            context.IsActive = user?.IsActive == true;

            return Task.CompletedTask;
        }
    }
}
