using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Datamodule
{
    public interface IChampionship : IDatamodul
    {
        string Name { get; set; }
        DateTime StartsAt { get; set; }
        DateTime EndsAt { get; set; }
    }
}
