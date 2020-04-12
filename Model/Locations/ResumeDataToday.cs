using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viam.SalesForceAPI.Model.Locations
{
    public class ResumeDataToday
    {

        public string idLocation { get; set; }
        private string ID_LOCATION
        {
            get { return idLocation; }
            set { idLocation = value.Trim(); }
        }

    }
}
