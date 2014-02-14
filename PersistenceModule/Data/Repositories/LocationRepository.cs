using Interfaces.PersisenceModule.Datamodule;
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
    class LocationRepository : ObservableCollection<ILocation>, ILocationRepository
    {
        AntelopeRestApi AntelopeRestApi { get; set; }

        public LocationRepository(AntelopeRestApi antelopeRestApi)
        {
            AntelopeRestApi = antelopeRestApi;
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    // post new items
                    var newItems = e.NewItems as List<ILocation>;
                    // var api = AntelopeRestApi.Instance.
                    foreach (var item in newItems)
                    {

                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    // delete new items
                    throw new NotSupportedException(e.Action.ToString());
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    // put (update) the items
                    break;
                default:
                    throw new NotSupportedException(e.Action.ToString() + " Action is not supported");
            }

            base.OnCollectionChanged(e);
        }

        public ILocation GetLocation(int id)
        {
            return AntelopeRestApi.GetLocation(id);
        }

        public ILocation PostLocation(ILocation location)
        {
            return AntelopeRestApi.PostLocation(location as Location);
        }

        public List<ILocation> GetLocations()
        {
            return new List<ILocation>(AntelopeRestApi.GetLocations());
        }
    }
}
