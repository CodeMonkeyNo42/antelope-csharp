using Interfaces.PersisenceModule.Datamodule;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Data.Datamodules
{
    class Team : NotificationObject, IDatamodul, ITeam
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

        public int championship_id;
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

        private int group_id;
        public int GroupId 
        {
            get
            {
                return group_id;
            }
            set
            {
                if (group_id != value)
                {
                    group_id = value;
                    RaisePropertyChanged("GroupId");
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
            return new { championship_id = ChampionshipId, group_id = GroupId };
        }

        public object GetPutObject()
        {
            return new { championship_id = ChampionshipId, group_id = GroupId };
        }

        public string GetRequestUrlPart()
        {
            return "championships/" + ChampionshipId + "/teams";
        }
    }
}
