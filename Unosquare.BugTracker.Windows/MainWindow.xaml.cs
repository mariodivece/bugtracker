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
using Unosquare.BugTracker.Shared;
using Unosquare.BugTracker.ViewModels;

namespace Unosquare.BugTracker.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RootViewModel = App.Current.Resources[nameof(RootViewModel)] as AppRoot;
            Loaded += MainWindow_Loaded;
        }

        private AppRoot RootViewModel { get; }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.UserInterfaceLoaded, null);
        }

        private void TakePicture_Click(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.TakePictureCommand, null);
        }

        private void PinsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RootViewModel.CurrentPicture.Pins.Add(new PinViewModel
            {
                Position = new Position { X = 40, Y = 40 }
            });
        }
    }
}
