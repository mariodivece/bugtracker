namespace Unosquare.BugTracker.Shared
{
    using System.Threading.Tasks;
    using Unosquare.BugTracker.ViewModels;

    /// <summary>
    /// Provides access to device-specific hardware
    /// </summary>
    public interface IDevice
    {
        Task<byte[]> TakePicture();
        Task<GpsLocationViewModel> ReadLocation();
    }
}
