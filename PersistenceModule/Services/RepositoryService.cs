using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Repositories;
using Interfaces.PersisenceModule.Services;
using Microsoft.Practices.Unity;
using PersistenceModule.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Services
{
    class RepositoryService : IRepositoryService
    {
        IUnityContainer UnityContainer { get; set; }

        public RepositoryService(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        private ILocationRepository locationRepository;
        public ILocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    locationRepository = UnityContainer.Resolve<ILocationRepository>();
                }
                return locationRepository;
            }
        }

        private INationRepository nationRepository;
        public INationRepository NationRepository
        {
            get
            {
                if (nationRepository == null)
                {
                    nationRepository = UnityContainer.Resolve<INationRepository>();
                }
                return nationRepository;
            }
        }

        private IChampionshipRepository championshipRepository;
        public IChampionshipRepository ChampionshipRepository
        {
            get
            {
                if (championshipRepository == null)
                {
                    championshipRepository = UnityContainer.Resolve<IChampionshipRepository>();
                }
                return championshipRepository;
            }
        }

        public ITeamRepository TeamRepository(IChampionship championship)
        {
            ITeamRepository teamRepository = UnityContainer.Resolve<ITeamRepository>();
            teamRepository.Championship = championship;
            return teamRepository;
        }

        
        public IGroupRepository GroupRepository(IChampionship championship)
        {
            IGroupRepository groupRepository = UnityContainer.Resolve<IGroupRepository>();
            groupRepository.Championship = championship;
            return groupRepository;
        }

        public IMatchRepository MatchRepository(IChampionship championship)
        {
            IMatchRepository matchRepository = UnityContainer.Resolve<IMatchRepository>();
            matchRepository.Championship = championship;
            return matchRepository;
        }
    }
}
