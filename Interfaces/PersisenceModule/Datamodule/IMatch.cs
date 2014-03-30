using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Datamodule
{
    public interface IMatch : IDatamodul
    {
        int ChampionshipId { get; set; }
        string Stage { get; set; }
        int Team1Id { get; set; }
        int Team2Id { get; set; }
        int LocationId { get; set; }
        DateTime StartsAt { get; set; }
    }
}
