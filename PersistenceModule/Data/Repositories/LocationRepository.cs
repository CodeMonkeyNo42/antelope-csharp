﻿using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Repositories;
using Microsoft.Practices.Unity;
using PersistenceModule.Api;
using PersistenceModule.Data.Datamodules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Repositories
{
    class LocationRepository : Repository<ILocation, Location>, ILocationRepository
    {
        public LocationRepository(AntelopeRestApi antelopeRestApi)
            : base(antelopeRestApi)
        {
        }
    }
}
