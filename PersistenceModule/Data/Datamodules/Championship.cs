using Interfaces.PersisenceModule.Datamodule;
using Microsoft.Practices.Prism.ViewModel;
using PersistenceModule.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PersistenceModule.Data.Datamodules
{
    [DataContract]
    class Championship : NotificationObject, IDatamodul, IChampionship
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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private int nationId;
        [DataMember(Name = "organizer_id")]
        public int NationId
        {
            get
            {
                return nationId;
            }
            set
            {
                nationId = value;
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

        [DataMember(Name = "starts_at")]
        public string StartsAtString
        {
            get
            {
                return StartsAt.ToString("s", CultureInfo.InvariantCulture);
            }
            set
            {
                StartsAt = DateTime.Parse(value);
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

        [DataMember(Name = "ends_at")]
        public string EndsAtString
        {
            get
            {
                return EndsAt.ToString("s");
            }
            set
            {
                EndsAt = DateTime.Parse(value);
            }
        }

        public string Duration
        {
            get
            {
                return StartsAt.ToString("dd.MM.yyyy") + " - " + EndsAt.ToString("dd.MM.yyyy");
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
            return new { organizer_id = NationId, starts_at = StartsAtString, ends_at = EndsAtString };
        }

        public object GetPutObject()
        {
            return new { organizer_id = NationId, starts_at = StartsAtString, ends_at = EndsAtString };
        }

        public string GetRequestUrlPart()
        {
            return "championships";
        }
    }
}
