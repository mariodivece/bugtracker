namespace Unosquare.BugTracker.Windows.Platform
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Unosquare.BugTracker.Shared;
    using Unosquare.BugTracker.ViewModels;

    public class MockDevice : IDevice
    {
        public async Task<GpsLocationViewModel> ReadLocation()
        {
            await Task.Delay(10);
            return GpsLocationViewModel.CreateRandom();
        }

        public async Task<byte[]> TakePicture()
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36";
                return await client.DownloadDataTaskAsync("https://loremflickr.com/g/320/240/insect");
            }
        }
    }
}
