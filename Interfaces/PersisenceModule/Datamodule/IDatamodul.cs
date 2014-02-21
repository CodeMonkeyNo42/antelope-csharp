using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Datamodule
{
    public interface IDatamodul
    {
        int Id { get; set; }
        string Url { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        object GetPostObject();
        object GetPutObject();
        string GetRequestUrlPart();
    }
}
