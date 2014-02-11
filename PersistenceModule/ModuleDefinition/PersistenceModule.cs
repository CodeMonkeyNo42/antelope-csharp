using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.ModuleDefinition
{
    class PersistenceModule : IModule
    {
        IServiceLocator ServiceLocator { get; set; }

        PersistenceModule(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }


        public void Initialize()
        {
            // add Views to regions
            // add Services
            throw new NotImplementedException();
        }
    }
}
