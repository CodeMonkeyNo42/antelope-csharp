using Interfaces.PersisenceModule.Datamodule;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Datamodules
{
    
    [DataContract]
    class Group : NotificationObject, IDatamodul, IGroup
    {
        private int id;
        [DataMember(Name = "id")]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }

        private string key;
        [DataMember(Name = "key")]
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                if (key != value)
                {
                    key = value;
                    RaisePropertyChanged("Key");
                }
            }
        }

        private DateTime created_at;
        public DateTime CreatedAt
        {
            get
            {
                return created_at;
            }
            set
            {
                if (created_at != value)
                {
                    created_at = value;
                    RaisePropertyChanged("CreatedAt");
                }
            }
        }

        private DateTime updated_at;
        public DateTime UpdatedAt
        {
            get
            {
                return updated_at;
            }
            set
            {
                if (updated_at != value)
                {
                    updated_at = value;
                    RaisePropertyChanged("UpdatedAt");
                }
            }
        }

        private string url;
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                if (url != value)
                {
                    url = value;
                    RaisePropertyChanged("Url");
                }
            }
        }

        public object GetPostObject()
        {
            return new { key = Key };
        }

        public object GetPutObject()
        {
            return new { key = Key };
        }

        public string GetRequestUrlPart()
        {
            return "groups";
        }
    }
}
