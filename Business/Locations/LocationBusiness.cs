using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viam.SalesForceAPI.Data.Locations;
using Viam.SalesForceAPI.Model.Configuration;
using Viam.SalesForceAPI.Model.Locations;

namespace Viam.SalesForceAPI.Business.Locations
{
    public class LocationBusiness
    {
        LocationRepository _envioReadRepository;

        public LocationBusiness(ConfigurationModel configuration)
        {
            _envioReadRepository = new LocationRepository(configuration.EnvioRead);
        }

        public ActionResult<List<ResumeData>> getResumeData(string idlocation, string idSalesRep, int measure)
        {
            return _envioReadRepository.getResumeData(idlocation, idSalesRep, measure);
        }

        public ActionResult<List<LocationModel>> getLocations(string idlocation, string idSalesRep, string synchronize)
        {
            return _envioReadRepository.getLocations(idlocation, idSalesRep, synchronize);
        }
    }
}
