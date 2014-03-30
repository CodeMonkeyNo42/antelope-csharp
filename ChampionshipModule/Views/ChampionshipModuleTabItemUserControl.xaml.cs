using ChampionshipModule.ViewModels;
using Interfaces.PersisenceModule.Datamodule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionshipModule.Views
{
    /// <summary>
    /// Interaktionslogik für ChampionshipModuleTabItemUserControl.xaml
    /// </summary>
    public partial class ChampionshipModuleTabItemUserControl : TabItem
    {
        public ChampionshipModuleTabItemUserControl(ChampionshipModuleTabItemUserControlViewModel championshipModuleTabItemUserControlViewModel)
        {
            InitializeComponent();

            Loaded += (o, e) => DataContext = championshipModuleTabItemUserControlViewModel;
        }

        private void TabItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Foreground = new SolidColorBrush(new Color() { R = 238, G = 124, B = 21, A = 255 });
        }

        private void TabItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
                Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
        }

        private void datagridchampionships_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                var championship = dgr.Item as IChampionship;
                
                var viewModel = DataContext as ChampionshipModuleTabItemUserControlViewModel;

                var tupel = new Tuple<IChampionship, ChampionshipModuleTabItemUserControl>(championship, tabitemchampionship);

                viewModel.RowDoubleClickCommand.Execute(tupel);
            }
        }
    }
}
