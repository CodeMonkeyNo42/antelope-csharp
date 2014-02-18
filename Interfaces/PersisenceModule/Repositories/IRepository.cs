using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.PersisenceModule.Repositories
{
    public interface IRepository<DatamoduleType>
    {
        DatamoduleType Get(int id);
        DatamoduleType Post(DatamoduleType location);
        DatamoduleType Put(DatamoduleType location);
        ObservableCollection<DatamoduleType> GetCollection();
    }
}
