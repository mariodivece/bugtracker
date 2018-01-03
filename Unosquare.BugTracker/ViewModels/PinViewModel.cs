using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unosquare.BugTracker.ViewModels
{
    public class PinViewModel : ViewModelBase
    {
        private Position m_Position;
        private bool m_IsBoundingBoxVisible;

        public BoundingBoxViewModel BoundingBox { get; } = new BoundingBoxViewModel();

        public bool IsBoundingBoxVisible
        {
            get { return m_IsBoundingBoxVisible; }
            set { SetProperty(ref m_IsBoundingBoxVisible, value); }
        }

        public Position Position
        {
            get { return m_Position; }
            set { SetProperty(ref m_Position, value); }
        }
    }
}
