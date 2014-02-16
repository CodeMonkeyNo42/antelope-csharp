﻿using LocationModule.ViewModels;
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

namespace LocationModule.Views
{
    /// <summary>
    /// Interaktionslogik für LocationModuleTabItemUserControl.xaml
    /// </summary>
    public partial class LocationModuleTabItemUserControl : TabItem
    {
        public LocationModuleTabItemUserControl(LocationModuleTabItemUserControlViewModel locationModuleTabItemUserControlViewModel)
        {
            InitializeComponent();

            Loaded += (o, e) => DataContext = locationModuleTabItemUserControlViewModel;

            // Foreground = new SolidColorBrush(new Color() { R = 238, G = 124, B = 21, A = 255 });
            // Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
        }
    }
}
