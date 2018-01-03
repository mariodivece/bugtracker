using System.Collections.ObjectModel;

namespace Unosquare.BugTracker.ViewModels
{
    /// <summary>
    /// Represents an observable collection of pins
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{Unosquare.BugTracker.ViewModels.PinViewModel}" />
    public class PinCollectionViewModel : ObservableCollection<PinViewModel> { }
}
