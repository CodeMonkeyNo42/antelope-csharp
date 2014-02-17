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

namespace AntelopeClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ItemCollection view = tabcontrol.Items;
            view.CurrentChanged += new EventHandler(TabControl_SelectionChanged);
        }


        /// <summary>
        /// TitleBar_MouseDown - Drag if single-click, resize if double-click
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
        }

        /// <summary>
        /// CloseButton_Clicked
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// MaximizedButton_Clicked
        /// </summary>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        /// <summary>
        /// Minimized Button_Clicked
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Adjusts the WindowSize to correct parameters when Maximize button is clicked
        /// </summary>
        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MaxButton.Content = "MAX";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MaxButton.Content = "is MAX";
            }

        }

        private void TabControl_SelectionChanged(object sender, EventArgs e)
        {
            var itemCollection = sender as ItemCollection;
            foreach (var item in itemCollection)
            {
                var tabItem = item as TabItem;
                if (tabItem == itemCollection.CurrentItem)
                {
                    // Foreground = new SolidColorBrush(new Color() { R = 238, G = 124, B = 21, A = 255 });
                    tabItem.Foreground = new SolidColorBrush(new Color() { R = 238, G = 124, B = 21, A = 255 });
                }
                else
                {
                    // Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
                    tabItem.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
                }

            }
            
        }

        

    }
}
