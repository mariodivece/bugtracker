namespace Unosquare.BugTracker
{
    using System;
    using System.Threading.Tasks;
    using Unosquare.BugTracker.Core;
    using Unosquare.BugTracker.Shared;
    using Unosquare.BugTracker.ViewModels;

    /// <summary>
    /// Represents the root application model
    /// </summary>
    /// <seealso cref="Unosquare.BugTracker.ViewModels.ViewModelBase" />
    public class AppRoot : ViewModelBase
    {
        private PictureHeaderViewModel m_SelectedPictureHeader = null;
        private PictureViewModel m_CurrentPicture = null;
        private string m_StatusMessage = string.Empty;
        public bool m_IsUserInterfaceEnabled = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRoot"/> class.
        /// </summary>
        public AppRoot()
        {
            // Resolve Platform-specific implementation instances
            StoreProvider = DependencyContainer.Instance.Resolve<IStoreProvider>();
            Device = DependencyContainer.Instance.Resolve<IDevice>();

            // Subscribe to message hub messages
            MessageHub.Subscribe(MessageNames.UserInterfaceLoaded, OnUserInterfaceLoaded);
            MessageHub.Subscribe(MessageNames.StatusAvailable, OnStatusAvailable);
            MessageHub.Subscribe(MessageNames.SelectedPictureHeader, OnSelectedPictureHeader);
            MessageHub.Subscribe(MessageNames.TakePictureCommand, OnTakePictureCommand);
        }

        #region Properties

        /// <summary>
        /// Gets the store provider.
        /// </summary>
        public IStoreProvider StoreProvider { get; }

        /// <summary>
        /// Gets the device.
        /// </summary>
        public IDevice Device { get; }

        /// <summary>
        /// Gets the message hub.
        /// </summary>
        public MessageHub MessageHub { get; } = MessageHub.Current;

        /// <summary>
        /// Gets the pictures.
        /// </summary>
        public PictureHeaderCollectionViewModel PictureHeaders { get; } = new PictureHeaderCollectionViewModel();

        /// <summary>
        /// Gets the application title.
        /// </summary>
        public string Title { get { return "Unosquare Bug Tracker - An approach to Deep Learning"; } }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user interface enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is user interface enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserInterfaceEnabled
        {
            get { return m_IsUserInterfaceEnabled; }
            private set { SetProperty(ref m_IsUserInterfaceEnabled, value); }
        }

        /// <summary>
        /// Gets or sets the selected picture header.
        /// </summary>
        public PictureHeaderViewModel SelectedPictureHeader
        {
            get { return m_SelectedPictureHeader; }
            set
            {
                SetProperty(ref m_SelectedPictureHeader, value);
                MessageHub.Publish(MessageNames.SelectedPictureHeader, value);
            }
        }

        /// <summary>
        /// Gets the current picture.
        /// </summary>
        public PictureViewModel CurrentPicture
        {
            get { return m_CurrentPicture; }
            private set { SetProperty(ref m_CurrentPicture, value); }
        }

        /// <summary>
        /// Gets the current status message.
        /// </summary>
        public string StatusMessage
        {
            get { return m_StatusMessage; }
            private set { SetProperty(ref m_StatusMessage, value); }
        }

        #endregion

        #region Message Hub Signal Handlers

        /// <summary>
        /// Called when [status available].
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private Task OnStatusAvailable(object payload)
        {
            var statusMessage = payload as string;
            if (statusMessage == null)
                StatusMessage = string.Empty;
            else
                StatusMessage = statusMessage;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when [user interface loaded].
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private async Task OnUserInterfaceLoaded(object payload)
        {
            try
            {
                IsUserInterfaceEnabled = false;
                MessageHub.Publish(MessageNames.StatusAvailable, "Loading Pictures . . .");
                var pictures = await StoreProvider.RetrievePictureHeadersAsync();
                foreach (var p in pictures)
                    PictureHeaders.Add(p);

                MessageHub.Publish(MessageNames.StatusAvailable, "Ready");
            }
            catch (Exception ex)
            {
                // We send a status message to the hub but it should really be an Error message of its own type
                MessageHub.Publish(MessageNames.StatusAvailable, $"Error: {ex.Message}");
            }
            finally
            {
                IsUserInterfaceEnabled = true;
            }

        }

        /// <summary>
        /// Called when [selected picture header].
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private async Task OnSelectedPictureHeader(object payload)
        {
            try
            {
                IsUserInterfaceEnabled = false;
                var picHeader = payload as PictureHeaderViewModel;
                MessageHub.Publish(MessageNames.StatusAvailable, $"Loading selected {picHeader.Name}, Taken on {picHeader.DateTakenUtc}");
                if (picHeader == null) return;

                // Save the picture before switching it.
                if (CurrentPicture != null)
                    await StoreProvider.AddOrUpdatePictureAsync(CurrentPicture);

                CurrentPicture = await StoreProvider.LoadPictureAsync(picHeader.PictureId);
                MessageHub.Publish(MessageNames.StatusAvailable, "Ready");
            }
            catch (Exception ex)
            {
                // We send a status message to the hub but it should really be an Error message of its own type
                MessageHub.Publish(MessageNames.StatusAvailable, $"Error: {ex.Message}");
            }
            finally
            {
                IsUserInterfaceEnabled = true;
            }

        }

        /// <summary>
        /// Called when [take picture command].
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        public async Task OnTakePictureCommand(object payload)
        {
            try
            {
                IsUserInterfaceEnabled = false;
                MessageHub.Publish(MessageNames.StatusAvailable, $"Taking picture from camera . . .");

                var pictureBytes = await Device.TakePicture();
                var pictureLocation = await Device.ReadLocation();

                var picture = new PictureViewModel
                {
                    ImageBytes = pictureBytes
                };

                picture.Header.DateTakenUtc = DateTime.UtcNow;
                picture.Header.Location.Latitude = pictureLocation.Latitude;
                picture.Header.Location.Longitude = pictureLocation.Longitude;
                picture.Header.Name = $"Taken at {DateTime.UtcNow.ToShortTimeString()}";
                picture.Header.PictureId = Guid.NewGuid();

                await StoreProvider.AddOrUpdatePictureAsync(picture);
                PictureHeaders.Add(picture.Header);
                SelectedPictureHeader = picture.Header;

                MessageHub.Publish(MessageNames.StatusAvailable, "Ready");
            }
            catch (Exception ex)
            {
                // We send a status message to the hub but it should really be an Error message of its own type
                MessageHub.Publish(MessageNames.StatusAvailable, $"Error: {ex.Message}");
            }
            finally
            {
                IsUserInterfaceEnabled = true;
            }
        }

        #endregion
    }
}
