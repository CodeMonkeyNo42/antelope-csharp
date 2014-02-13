using Interfaces.PersisenceModule.Datamodule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Repositories
{
    public interface ILocationRepository : INotifyCollectionChanged, INotifyPropertyChanged, ICollection<ILocation>
    { 
    }
}
