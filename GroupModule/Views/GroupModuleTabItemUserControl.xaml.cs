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

namespace GroupModule.Views
{
    /// <summary>
    /// Interaktionslogik für GroupModuleTabItemUserControl.xaml
    /// </summary>
    public partial class GroupModuleTabItemUserControl : TabItem
    {
        public GroupModuleTabItemUserControl()
        {
            InitializeComponent();
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
    }
}
