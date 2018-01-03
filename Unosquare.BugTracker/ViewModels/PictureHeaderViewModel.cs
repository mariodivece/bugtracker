namespace Unosquare.BugTracker.ViewModels
{
    using System;

    public class PictureHeaderViewModel : ViewModelBase
    {
        private Guid m_PictureId;
        private string m_Name;
        private DateTime m_DateTakenUtc;
        private GpsLocationViewModel m_Location = new GpsLocationViewModel();

        public Guid PictureId
        {
            get { return m_PictureId; }
            set { SetProperty(ref m_PictureId, value); }
        }

        public string Name
        {
            get { return m_Name; }
            set { SetProperty(ref m_Name, value); }
        }

        public DateTime DateTakenUtc
        {
            get { return m_DateTakenUtc; }
            set { SetProperty(ref m_DateTakenUtc, value); }
        }

        public GpsLocationViewModel Location
        {
            get { return m_Location; }
        }
    }
}
