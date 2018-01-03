namespace Unosquare.BugTracker.Shared
{
    using System;
    using System.Threading.Tasks;
    using Unosquare.BugTracker.ViewModels;

    public interface IStoreProvider
    {
        Task<PictureHeaderViewModel[]> RetrievePictureHeadersAsync();

        Task<PictureViewModel> LoadPictureAsync(Guid pictureId);

        Task<Guid> AddOrUpdatePictureAsync(PictureViewModel picture);
    }
}
