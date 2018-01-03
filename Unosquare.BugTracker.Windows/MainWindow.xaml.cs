namespace Unosquare.BugTracker.Windows
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Unosquare.BugTracker.Shared;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            RootViewModel = App.Current.Resources[nameof(RootViewModel)] as AppRoot;
            Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// Gets the root view model.
        /// </summary>
        /// <value>
        /// The root view model.
        /// </value>
        private AppRoot RootViewModel { get; }

        /// <summary>
        /// Handles the Loaded event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.UserInterfaceLoaded, null);
        }

        /// <summary>
        /// Handles the Click event of the TakePicture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TakePicture_Click(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.TakePictureCommand, null);
        }

        /// <summary>
        /// Handles the Click event of the DropPin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DropPin_Click(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.DropPinCommand, new Tuple<double, double>(0, 0));
        }

        /// <summary>
        /// Handles the Click event of the FixPin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FixPin_Click(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.FixPinCommand, null);
        }

        /// <summary>
        /// Handles the Click event of the MarkIdentified control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MarkIdentified_Click(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.MarkIdentifiedCommand, null);
        }

        /// <summary>
        /// Handles the Click event of the ToggleBoundingBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ToggleBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.ToggleBoundingBoxCommand, null);
        }

        /// <summary>
        /// Handles the MouseLeftDown event of the CurrentImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void CurrentImage_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(CurrentImage);
            var offset = PinThumb.ActualWidth / 2.0;
            RootViewModel.MessageHub.Publish(MessageNames.DropPinCommand, new Tuple<double, double>(position.X - offset, position.Y - offset));
        }

        /// <summary>
        /// Handles the DragDelta event of the PinThumb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.Primitives.DragDeltaEventArgs"/> instance containing the event data.</param>
        private void PinThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (RootViewModel.CurrentPicture == null) return;

            var x = Canvas.GetLeft(PinThumb) + e.HorizontalChange;
            if (x < 0) x = 0;
            if (x > CurrentImage.ActualWidth) x = CurrentImage.ActualWidth;

            var y = Canvas.GetTop(PinThumb) + e.VerticalChange;
            if (y < 0) y = 0;
            if (y > CurrentImage.ActualHeight) y = CurrentImage.ActualHeight;

            Canvas.SetLeft(PinThumb, x);
            Canvas.SetTop(PinThumb, y);
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the PinThumb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void PinThumb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RootViewModel.MessageHub.Publish(MessageNames.ToggleBoundingBoxCommand, null);
        }
    }
}
