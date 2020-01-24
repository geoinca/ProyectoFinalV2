using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
 
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace INSOLMIMARC.Academy.Web
{
    public class TokenClient
    {
        public TokenClient(HttpClient client, IOptions<TokenClientOptions> options)
        {
            Client = client;
            Options = options.Value;
        }

        public HttpClient Client { get; }
        public TokenClientOptions Options { get; }

        public async Task<string> GetToken()
        {
            var response = await Client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = Options.Address,
                ClientId = Options.ClientId,
                ClientSecret = Options.ClientSecret
            });

            return response.AccessToken ?? response.Error;
        }
    }
}
