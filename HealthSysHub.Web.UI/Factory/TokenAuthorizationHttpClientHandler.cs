using HealthSysHub.Web.UI.Models;
using Microsoft.Extensions.Options;

namespace HealthSysHub.Web.UI.Factory
{
    public class TokenAuthorizationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HealthSysHubConfig _chtsConfigConfig;


        public TokenAuthorizationHttpClientHandler(IHttpContextAccessor httpContextAccessor, IOptions<HealthSysHubConfig> healthSysHubConfigConfigConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _chtsConfigConfig = healthSysHubConfigConfigConfig.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Add("Authorization", accessToken);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
