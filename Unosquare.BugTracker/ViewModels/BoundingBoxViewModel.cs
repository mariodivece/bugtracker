namespace Unosquare.BugTracker.ViewModels
{
    public class BoundingBoxViewModel : ViewModelBase
    {
        private double m_X;
        private double m_Y;
        private double m_W;
        private double m_H;

        public double X
        {
            get { return m_X; }
            set { SetProperty(ref m_X, value); }
        }

        public double Y
        {
            get { return m_Y; }
            set { SetProperty(ref m_Y, value); }
        }

        public double W
        {
            get { return m_W; }
            set { SetProperty(ref m_W, value); }
        }

        public double H
        {
            get { return m_H; }
            set { SetProperty(ref m_H, value); }
        }

    }
}
