namespace Unosquare.BugTracker.ViewModels
{
    using System;

    public class GpsLocationViewModel : ViewModelBase
    {
        private double m_Longitude;
        private double m_Latitude;

        public double Longitude
        {
            get { return m_Longitude; }
            set { SetProperty(ref m_Longitude, value); }
        }

        public double Latitude
        {
            get { return m_Latitude; }
            set { SetProperty(ref m_Latitude, value); }
        }

        public static GpsLocationViewModel CreateRandom()
        {
            var random = new Random();
            var location = new GpsLocationViewModel
            {
                Latitude = random.NextDouble(),
                Longitude = random.NextDouble()
            };

            return location;
        }
    }
}
