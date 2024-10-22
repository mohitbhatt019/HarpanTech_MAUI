using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Response
{
    public class ContainerResponse
    {
        [JsonProperty("eirNo")]
        public int EIRNo { get; set; }

        [JsonProperty("containerNo")]
        public string ContainerNo { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("turnInDate")]
        public DateTime TurnInDate { get; set; }

        [JsonProperty("isoType")]
        public string ISOType { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("dispatchDate")]
        public DateTime? DispatchDate { get; set; }
    }
}
