using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace Biblioteka
{


    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public event EventHandler<CustomEventArgs> MaximizeEvent;

        public Settings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Fullscreen = (bool)cbIsFullscreen.IsChecked;
            Properties.Settings.Default.Save();
            MaximizeEvent(this, new CustomEventArgs());
            Close();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbIsFullscreen.IsChecked = Properties.Settings.Default.Fullscreen;
        }

    }
}