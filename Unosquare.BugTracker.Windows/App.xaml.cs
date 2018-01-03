namespace Unosquare.BugTracker.Windows
{
    using System.Windows;
    using Unosquare.BugTracker.Core;
    using Unosquare.BugTracker.Shared;
    using Unosquare.BugTracker.Windows.Platform;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Register platform-specific implementation
            DependencyContainer.Instance.Register<IStoreProvider>(new MockStoreProvider());
            DependencyContainer.Instance.Register<IDevice>(new MockDevice());
            base.OnStartup(e);
        }
    }
}
