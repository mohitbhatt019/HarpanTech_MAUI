using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Repair
{
    // RepairModel class represents a model for information about repairs needed
    public class RepairModel
    {
        // Gets or sets the unique identifier for the repair information
        public int Id { get; set; }

        // Gets or sets a flag indicating whether repair is needed
        public bool RepairNeeded { get; set; }

        // Various flags indicating specific repair needs
        public bool Major { get; set; }
        public bool Wash { get; set; }
        public bool WasteMaterial { get; set; }
        public bool Gasket { get; set; }
        public bool CorrosionPinhole { get; set; }
        public bool Cut { get; set; }
        public bool Dent { get; set; }
        public bool BentLocking { get; set; }
        public bool FloorLoose { get; set; }
        public bool FloorDamageFloorDelam { get; set; }
        public bool UnderStructureFloor { get; set; }
    }
}
