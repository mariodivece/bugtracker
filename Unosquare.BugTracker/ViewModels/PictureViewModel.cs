namespace Unosquare.BugTracker.ViewModels
{
    using System;

    /// <summary>
    /// Holds picture data, dimensions, 
    /// </summary>
    /// <seealso cref="Unosquare.BugTracker.ViewModels.ViewModelBase" />
    public class PictureViewModel : ViewModelBase
    {
        private double m_ImageWidth;
        private double m_ImageHeight;

        public PictureHeaderViewModel Header { get; } = new PictureHeaderViewModel();

        public PinViewModel Pin { get; } = new PinViewModel();

        public byte[] ImageBytes { get; set; }

        public double ImageWidth
        {
            get { return m_ImageWidth; }
            set { SetProperty(ref m_ImageWidth, value); }
        }

        public double ImageHeight
        {
            get { return m_ImageHeight; }
            set { SetProperty(ref m_ImageHeight, value); }
        }
    }
}
