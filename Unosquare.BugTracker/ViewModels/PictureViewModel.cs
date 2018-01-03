namespace Unosquare.BugTracker.ViewModels
{
    using System;

    /// <summary>
    /// Holds picture data, dimensions, 
    /// </summary>
    /// <seealso cref="Unosquare.BugTracker.ViewModels.ViewModelBase" />
    public class PictureViewModel : ViewModelBase
    {
        public PictureHeaderViewModel Header { get; } = new PictureHeaderViewModel();

        public PinCollectionViewModel Pins { get; } = new PinCollectionViewModel();

        public byte[] ImageBytes { get; set; }
    }
}
