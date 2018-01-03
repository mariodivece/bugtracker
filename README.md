# Bug Tracker Challenge
A proof of concept application describing several architectural patterns

<img src="https://github.com/mariodivece/bugtracker/raw/master/screenshot.png" />

## Architectural Overview
- A core library controls the entire application. It is platform independent.
- This core library has a message hub so that messages can be sent between the lib and the front-end UI. The Message hub defines a very simple delegate which is forced to be asynchronous to prevent the UI from blocking.
- The core library also has a dependency injection container. This allows for retrieval of platform-specific functionality
- The state is controlled entirely via ViewModels. Whenever a property changes, the UI changes
- The platform-specific abstractions include the database access, and hardware access to the GPS and the camera.
- The rendering of the visuals is completely up to the front-end. It was easy to setup because the graphical properties of the visual elements are bound to the underlying ViewModel!
- The async/await pattern is used whenever possible to avoid blocking the UI.
- Please note that patterns such as ViewModelBase, Signletons, MessageHub, and DependencyContainer wer over-simplified. We have a very cool library fro all of that: https://github.com/unosquare/swan
- Exception handling is very poor at this point in order to reduce implementation time.

That's all. Enjoy!
-Mario
