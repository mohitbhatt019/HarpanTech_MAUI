using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Dispatch
{
    // DispatchEdit class represents information about a dispatch edit screen
    public class DispatchEdit
    {
        // Gets or sets the customer associated with the dispatch
        public string Customer { get; set; }

        // Gets or sets the booking reference for the dispatch
        public int BookingReference { get; set; }

        // Gets or sets the container number for the dispatch
        public int ContainerNumber { get; set; }

        // Gets or sets the ISO code for the dispatch
        public int ISO { get; set; }

        // Gets or sets the temperature instruction for the dispatch
        public int TempInstruction { get; set; }

        // Gets or sets the temperature in degrees Celsius for the dispatch
        public int TempInDegreeCelsius { get; set; }

        // Gets or sets the seal information for the dispatch
        public string Seal1 { get; set; }

        // Gets or sets the seal information for the dispatch
        public string Seal2 { get; set; }
    }
}
