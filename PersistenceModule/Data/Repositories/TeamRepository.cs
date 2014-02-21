﻿using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Repositories;
using PersistenceModule.Api;
using PersistenceModule.Data.Datamodules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Repositories
{

    class TeamRepository : Repository<ITeam, Team>, ITeamRepository
    {
        public TeamRepository(AntelopeRestApi antelopeRestApi)
            : base(antelopeRestApi)
        {
        }
    }
}
