using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HarpenTech.Models.Response
{
   

       public class ReceiveItemResponse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("isComplete")]
        public bool IsComplete { get; set; }

        [JsonPropertyName("recieveTestModelId")]
        public long RecieveTestModelId { get; set; }

        [JsonPropertyName("containerNo")]
        public string ContainerNo { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }

        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        [JsonPropertyName("Image")]
        public string Image { get; set; }


    }

    public class ReceiveItems
    {
        public List<ReceiveItemResponse> items { get; set; }
    }
  
}
