using Microsoft.Datasync.Client;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Container
{
    public class ContainerReciever:DatasyncClientData, IEquatable<ContainerReciever>
    {
        [PrimaryKey, AutoIncrement]
        public int ContainerRecieverId { get; set; }
        public string ContainerNo { get; set; }
        public string Customer { get; set; }
        public string Remarks { get; set; }

        public string Image { get; set; }

        public string UpdatedAtInString { get; set; } 

        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public bool Equals(ContainerReciever other)
            => other != null && other.Id == Id && other.Title == Title && other.IsComplete == IsComplete && other.Image == Image;

        public ContainerReciever Clone() => MemberwiseClone() as ContainerReciever;

        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Customer))
            {
                return (false, $"{nameof(Customer)} is required.");
            }
            else if (ContainerNo.Length <= 0)
            {
                return (false, $"{nameof(ContainerNo)} should be greater than 0.");
            }
            return (true, null);
        }


    }
}
