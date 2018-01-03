namespace Unosquare.BugTracker.Shared
{
    /// <summary>
    /// Defines the names for the message hub messages
    /// </summary>
    public static class MessageNames
    {
        public const string UserInterfaceLoaded = nameof(UserInterfaceLoaded);
        public const string StatusAvailable = nameof(StatusAvailable);
        public const string SelectedPictureHeader = nameof(SelectedPictureHeader);
        public const string TakePictureCommand = nameof(TakePictureCommand);
    }
}
