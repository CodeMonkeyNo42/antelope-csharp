using Interfaces.PersisenceModule.Datamodule;
using PersistenceModule.Api;
using PersistenceModule.Data.Datamodules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Repositories
{
    class Repository<InterfaceDatamoduleType, DatamoduleType>
        where InterfaceDatamoduleType : IDatamodul
        where DatamoduleType : class, IDatamodul, InterfaceDatamoduleType, new()
    {
        AntelopeRestApi AntelopeRestApi { get; set; }
        //ConditionalWeakTable<int, BindingList<InterfaceDatamoduleType>> WeakreferenceList { get; set; }

        public Repository(AntelopeRestApi antelopeRestApi)
        {
            AntelopeRestApi = antelopeRestApi;
        }

        public InterfaceDatamoduleType Get(int id)
        {
            var datamodul = AntelopeRestApi.Get<DatamoduleType>(id);
            SetComputedProperties(datamodul);
            return datamodul;
        }

        public InterfaceDatamoduleType Post(InterfaceDatamoduleType datamodul)
        {
            return AntelopeRestApi.Post<DatamoduleType>(datamodul as DatamoduleType);
        }

        public InterfaceDatamoduleType Put(InterfaceDatamoduleType datamodul)
        {
            return AntelopeRestApi.Put<DatamoduleType>(datamodul as DatamoduleType);
        }

        public BindingList<InterfaceDatamoduleType> GetCollection()
        {
            var list = AntelopeRestApi.GetCollection<DatamoduleType>();
            var bindingList = new BindingList<InterfaceDatamoduleType>( new List<InterfaceDatamoduleType>(list) );

            foreach (var item in bindingList)
            {
                SetComputedProperties(item);
                //item.PropertyChanged += OnPropertyChanged;
            }

            bindingList.ListChanged += OnCollectionChanged;
            bindingList.RaiseListChangedEvents = true;

            return bindingList;
        }

        void OnCollectionChanged(object sender, ListChangedEventArgs e)
        {
            var list = sender as BindingList<InterfaceDatamoduleType>;

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    // post (insert) new items
                    var newItem = list[e.NewIndex];
                    AntelopeRestApi.Post<DatamoduleType>(newItem as DatamoduleType);
                    
                    break;
                case ListChangedType.ItemChanged:
                    // put (update) the items
                    var changedItem = list[e.OldIndex];
                    AntelopeRestApi.Put<DatamoduleType>(changedItem as DatamoduleType);
                    
                    break;
                default:
                    throw new NotSupportedException(e.ListChangedType.ToString() + " Action is not supported");
            }
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var newItem = AntelopeRestApi.Put<DatamoduleType>(sender as DatamoduleType);
        }

        protected virtual void SetComputedProperties(InterfaceDatamoduleType datamodul)
        {
        }
    }
}
