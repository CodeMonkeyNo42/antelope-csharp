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
    class Nation : NotificationObject, IDatamodul, INation
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

        private string name;
        [DataMember(Name = "name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private string continent;
        [DataMember(Name = "continent")]
        public string Continent
        {
            get
            {
                return continent;
            }
            set
            {
                if (continent != value)
                {
                    continent = value;
                    RaisePropertyChanged("Continent");
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
            return new { name = Name };
        }

        public object GetPutObject()
        {
            return new { name = Name };
        }

        public string GetRequestUrlPart()
        {
            return "nations";
        }
    }
}
