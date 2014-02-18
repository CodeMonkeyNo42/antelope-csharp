using Interfaces.PersisenceModule.Datamodule;
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
    class Repository<InterfaceDatamoduleType, DatamoduleType>
        where InterfaceDatamoduleType : IDatamodul
        where DatamoduleType : class, IDatamodul, InterfaceDatamoduleType, new()
    {
        AntelopeRestApi AntelopeRestApi { get; set; }

        public Repository(AntelopeRestApi antelopeRestApi)
        {
            AntelopeRestApi = antelopeRestApi;
        }

        public InterfaceDatamoduleType Get(int id)
        {
            return AntelopeRestApi.Get<DatamoduleType>(id);
        }

        public InterfaceDatamoduleType Post(InterfaceDatamoduleType location)
        {
            return AntelopeRestApi.Post<DatamoduleType>(location as DatamoduleType);
        }

        public InterfaceDatamoduleType Put(InterfaceDatamoduleType location)
        {
            return AntelopeRestApi.Put<DatamoduleType>(location as DatamoduleType);
        }

        public ObservableCollection<InterfaceDatamoduleType> GetCollection()
        {
            var observableCollection = new ObservableCollection<InterfaceDatamoduleType>(AntelopeRestApi.GetCollection<DatamoduleType>());

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
                        AntelopeRestApi.Post<DatamoduleType>(item as DatamoduleType);
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
                        AntelopeRestApi.Put<DatamoduleType>(item as DatamoduleType);
                    }
                    break;
                default:
                    throw new NotSupportedException(e.Action.ToString() + " Action is not supported");
            }
        }
    }
}
