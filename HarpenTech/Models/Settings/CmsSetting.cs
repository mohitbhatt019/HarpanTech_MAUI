using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Settings
{
    public class CmsSetting
    {
        [PrimaryKey, AutoIncrement]
        public int CmsSettingId { get; set; }
        public string Url { get; set; }
        public string CompanyName { get; set; }
        public string MainDb { get; set; }
        public string Depot { get; set; }

        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public bool Equals(CmsSetting other)
            => other != null && other.Url == Url && other.Title == Title && other.IsComplete == IsComplete && other.CompanyName == CompanyName
             && other.MainDb == MainDb && other.Depot == Depot;

        public CmsSetting Clone() => MemberwiseClone() as CmsSetting;

        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return (false, $"{nameof(Url)} is required.");
            }
            else if (CompanyName.Length <= 0)
            {
                return (false, $"{nameof(CompanyName)} should be greater than 0.");
            }
            else if(string.IsNullOrWhiteSpace(MainDb))
            {
                return (false, $"{nameof(MainDb)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(Depot))
            {
                return (false, $"{nameof(Depot)} is required.");
            }

            return (true, null);
        }
    }
}
