using Interfaces.PersisenceModule.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Services
{
    public interface IRepositoryService
    {
        ILocationRepository LocationRepository { get; }
        INationRepository NationRepository { get; }
        IChampionshipRepository ChampionshipRepository { get; }
    }
}
