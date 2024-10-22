using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models
{
    // TokenResponse class represents the response received after a token request
    public class TokenResponse
    {
        // Gets or sets the access token received from the authorization server
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        // Gets or sets the type of the token (e.g., "Bearer")
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        // Gets or sets the duration in seconds for which the token is valid
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        // Gets or sets the identity token associated with the user
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        // Gets or sets the refresh token used to obtain a new access token
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
