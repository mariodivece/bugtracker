using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unosquare.BugTracker.ViewModels
{
    public class PinViewModel : ViewModelBase
    {
        private bool m_IsBoundingBoxVisible;
        private bool m_IsActive;
        private bool m_IsBugIdentified;
        private double m_X;
        private double m_Y;

        public double X
        {
            get { return m_X; }
            set
            {
                SetProperty(ref m_X, value);
                BoundingBox.X = m_X - (BoundingBox.W / 2.0);
            }
        }

        public double Y
        {
            get { return m_Y; }
            set
            {
                SetProperty(ref m_Y, value);
                BoundingBox.Y = m_Y - (BoundingBox.H / 2.0);
            }
        }

        public BoundingBoxViewModel BoundingBox { get; } = new BoundingBoxViewModel { W = 50, H = 50 };

        public bool IsBoundingBoxVisible
        {
            get { return m_IsBoundingBoxVisible; }
            set { SetProperty(ref m_IsBoundingBoxVisible, value); }
        }

        public bool IsActive
        {
            get { return m_IsActive; }
            set { SetProperty(ref m_IsActive, value); }
        }

        public bool IsBugIdentified
        {
            get { return m_IsBugIdentified; }
            set { SetProperty(ref m_IsBugIdentified, value); }
        }
    }
}
