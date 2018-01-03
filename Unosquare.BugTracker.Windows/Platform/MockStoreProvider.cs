namespace Unosquare.BugTracker.Windows.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Unosquare.BugTracker.Shared;
    using Unosquare.BugTracker.ViewModels;

    public class MockStoreProvider : IStoreProvider
    {
        private List<PictureViewModel> StoredPictures = new List<PictureViewModel>();

        public MockStoreProvider()
        {
            for (var i = 0; i < 10; i++)
            {
                var picture = new PictureViewModel();
                picture.Header.DateTakenUtc = DateTime.UtcNow.AddDays(-i);
                picture.Header.Name = $"Nature Picture {i}";
                picture.Header.PictureId = Guid.NewGuid();

                var location = GpsLocationViewModel.CreateRandom();
                picture.Header.Location.Latitude = location.Latitude;
                picture.Header.Location.Longitude = location.Longitude;

                StoredPictures.Add(picture);
            }
        }

        public async Task<PictureViewModel> LoadPictureAsync(Guid pictureId)
        {
            
            var existingPicture = StoredPictures.Where(p => p.Header.PictureId == pictureId).FirstOrDefault();
            if (existingPicture?.ImageBytes == null)
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36";
                    existingPicture.ImageBytes = await client.DownloadDataTaskAsync("https://loremflickr.com/g/320/240/insect");
                }
            }

            return existingPicture;
        }

        public async Task<Guid> AddOrUpdatePictureAsync(PictureViewModel picture)
        {
            var existingPicture = StoredPictures.Where(p => p.Header.PictureId == picture.Header.PictureId).FirstOrDefault();
            if (existingPicture == null)
                StoredPictures.Add(picture);
            else
            {
                existingPicture.Header.DateTakenUtc = picture.Header.DateTakenUtc;
                existingPicture.Header.Location.Latitude = picture.Header.Location.Longitude;
                existingPicture.Header.Location.Longitude = picture.Header.Location.Longitude;
                existingPicture.Header.Name = picture.Header.Name;
                existingPicture.Header.PictureId = picture.Header.PictureId;
                existingPicture.Pins.Clear();
                foreach (var pin in picture.Pins)
                    existingPicture.Pins.Add(pin);

                picture = existingPicture;
            }

            await Task.Delay(10);
            return picture.Header.PictureId;
        }

        public async Task<PictureHeaderViewModel[]> RetrievePictureHeadersAsync()
        {
            await Task.Delay(2000);
            return StoredPictures.Select(p => p.Header).ToArray();
        }
    }
}
