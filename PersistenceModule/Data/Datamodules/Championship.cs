using Interfaces.PersisenceModule.Datamodule;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Datamodules
{
    class Championship : NotificationObject, IDatamodul, IChampionship
    {
        private int id;
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

        private DateTime starts_at;
        public DateTime StartsAt
        {
            get
            {
                return starts_at;
            }
            set
            {
                if(starts_at != value)
                {
                    starts_at = value;
                    RaisePropertyChanged("StartsAt");
                }
            }
        }

        private DateTime ends_at;
        public DateTime EndsAt
        {
            get
            {
                return ends_at;
            }
            set
            {
                if(ends_at != value)
                {
                    ends_at = value;
                    RaisePropertyChanged("EndsAt");
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
            return new { name = Name, starts_at = StartsAt, ends_at = EndsAt };
        }

        public object GetPutObject()
        {
            return new { name = Name, starts_at = StartsAt, ends_at = EndsAt };
        }

        public string GetRequestUrlPart()
        {
            return "championships";
        }
    }
}
