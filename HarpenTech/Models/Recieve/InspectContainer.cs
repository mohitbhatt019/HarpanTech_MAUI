using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Recieve
{
   public class InspectContainer
    {
        public string Status { get; set; }
        public string Grade { get; set; }
        public string Damage { get; set; }
        public List<DamageListClass> DamageListClasses { get; set; }

    }

    public class DamageListClass
    {
        public string Damage { get; set; }

    }

}
