using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AntelopeClient
{
    class MyBootstrapper : UnityBootstrapper
    {
        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override System.Windows.DependencyObject CreateShell()
        {
            return new MainWindow(Container as IUnityContainer);
        }

        protected override void InitializeShell()
        {
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
    }
}
