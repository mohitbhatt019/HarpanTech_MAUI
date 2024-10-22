using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Response
{
    public class MockTestConnectionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        // Gets or sets the type of the token (e.g., "Bearer")
        [JsonProperty("harpan")]
        public string Harpan { get; set; }
    }
}
