using Interfaces.PersisenceModule.Datamodule;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Datamodules
{
    
    [DataContract]
    class Match : NotificationObject, IDatamodul, IMatch
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

        public int championship_id;
        [DataMember(Name = "championship_id")]
        public int ChampionshipId
        {
            get
            {
                return championship_id;
            }
            set
            {
                if (championship_id != value)
                {
                    championship_id = value;
                    RaisePropertyChanged("ChampionshipId");
                }
            }
        }

        private string stage;
        [DataMember(Name = "stage")]
        public string Stage
        {
            get
            {
                return stage;
            }
            set
            {
                if (stage != value)
                {
                    stage = value;
                    RaisePropertyChanged("Stage");
                }
            }
        }


        private int team1Id;
        [DataMember(Name = "team1_id")]
        public int Team1Id
        {
            get
            {
                return team1Id;
            }
            set
            {
                if (team1Id != value)
                {
                    team1Id = value;
                    RaisePropertyChanged("Team1Id");
                }
            }
        }

        private int team2Id;
        [DataMember(Name = "team2_id")]
        public int Team2Id
        {
            get
            {
                return team2Id;
            }
            set
            {
                if (team2Id != value)
                {
                    team2Id = value;
                    RaisePropertyChanged("Team2Id");
                }
            }
        }


        private int locationId;
        [DataMember(Name = "location_id")]
        public int LocationId
        {
            get
            {
                return locationId;
            }
            set
            {
                if (locationId != value)
                {
                    locationId = value;
                    RaisePropertyChanged("LocationId");
                }
            }
        }

        private DateTime startsAt;
        public DateTime StartsAt
        {
            get
            {
                return startsAt;
            }
            set
            {
                if (startsAt != value)
                {
                    startsAt = value;
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
            return new { championship_id = ChampionshipId };
        }

        public object GetPutObject()
        {
            return new { championship_id = ChampionshipId };
        }

        public string GetRequestUrlPart()
        {
            return "matches";
        }
    }
}
