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
    class LocationRepository : ILocationRepository
    {
        AntelopeRestApi AntelopeRestApi { get; set; }

        public LocationRepository(AntelopeRestApi antelopeRestApi)
        {
            AntelopeRestApi = antelopeRestApi;
        }

        public ILocation GetLocation(int id)
        {
            return AntelopeRestApi.GetLocation(id);
        }

        public ILocation PostLocation(ILocation location)
        {
            return AntelopeRestApi.PostLocation(location as Location);
        }

        public ILocation PutLocation(ILocation location)
        {
            return AntelopeRestApi.PutLocation(location as Location);
        }

        public List<ILocation> GetLocations()
        {
            // ObservableCollection<ILocation>
            return new List<ILocation>(AntelopeRestApi.GetLocations());
        }

        public ObservableCollection<ILocation> GetLocations2()
        {
            var observableCollection = new ObservableCollection<ILocation>(AntelopeRestApi.GetLocations());

            observableCollection.CollectionChanged += OnCollectionChanged;

            return observableCollection;
        }

        void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    // post new items
                    foreach (var item in e.NewItems)
                    {
                        AntelopeRestApi.PostLocation(item as Location);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    // delete new items
                    throw new NotSupportedException(e.Action.ToString());
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    // put (update) the items
                    foreach (var item in e.OldItems)
                    {
                        AntelopeRestApi.PutLocation(item as Location);
                    }
                    break;
                default:
                    throw new NotSupportedException(e.Action.ToString() + " Action is not supported");
            }
        }
    }
}
