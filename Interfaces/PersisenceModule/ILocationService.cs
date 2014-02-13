using Interfaces.PersisenceModule;
using Interfaces.PersisenceModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.LocationModule
{
    public interface ILocationService
    {
        ILocationRepository Locations { get; }
    }
}
