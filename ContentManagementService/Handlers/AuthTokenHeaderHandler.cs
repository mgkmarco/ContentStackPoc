using ContentManagementService.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagementService.Handlers
{
    public class AuthTokenHeaderHandler : DelegatingHandler
    {
        public readonly TokenOptions _tokenOptions;
        
        public AuthTokenHeaderHandler(IOptionsMonitor<ContentStackOptions> optionsMonitor)
        {
            _tokenOptions = optionsMonitor.CurrentValue.Tokens;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("api_key", _tokenOptions.Api);
            request.Headers.Add("authorization", _tokenOptions.Management);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}