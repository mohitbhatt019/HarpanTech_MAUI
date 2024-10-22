using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Models.Models.ApiData
{
    // ApiData class represents data obtained from an API response
    public class ApiData
    {
        // Gets or sets a flag indicating whether the user is an administrator
        public bool isAdministrator { get; set; }

        // Gets or sets an array of roles associated with the user
        public string[] roles { get; set; }

        // Gets or sets an array of facilities associated with the user
        public object[] facilities { get; set; }

        // Gets or sets information about the application
        public Application application { get; set; }
    }

    // Application class represents information about the application
    public class Application
    {
        // Gets or sets the version code of the application
        public int versionCode { get; set; }

        // Gets or sets the version string of the application
        public string version { get; set; }

        // Gets or sets the name of the application
        public string name { get; set; }

        // Gets or sets the URL associated with the application
        public string url { get; set; }
    }

}
