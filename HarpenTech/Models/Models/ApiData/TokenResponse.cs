using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HarpenTech.Models.Models.ApiData
{
    // TokenResponse class represents the response received after a token request
    public class TokenResponse
    {
        // Gets or sets the access token received from the authorization server
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        // Gets or sets the type of the token (e.g., "Bearer")
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        // Gets or sets the duration in seconds for which the token is valid
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
