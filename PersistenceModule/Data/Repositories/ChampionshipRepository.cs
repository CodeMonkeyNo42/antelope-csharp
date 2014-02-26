using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Repositories;
using Interfaces.PersisenceModule.Services;
using PersistenceModule.Api;
using PersistenceModule.Data.Datamodules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Repositories
{
    class ChampionshipRepository : Repository<IChampionship, Championship>, IChampionshipRepository
    {
        public ChampionshipRepository(AntelopeRestApi antelopeRestApi, IRepositoryService repositoryService)
            : base(antelopeRestApi)
        {
            RepositoryService = repositoryService;
        }

        private IRepositoryService RepositoryService { get; set; }

        protected override void SetComputedProperties(IChampionship datamodul)
        {
            base.SetComputedProperties(datamodul);
            var nation = RepositoryService.NationRepository.Get(datamodul.NationId);
            datamodul.Name = nation.Name + " " + datamodul.StartsAt.Year.ToString();
        }
    }
}
