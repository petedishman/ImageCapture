using System;

namespace ImageCapture
{
    class MainWindowViewModelFactory : IMainWindowViewModelFactory
    {
        private IClipboardMonitor clipboardMonitor;
        private IClipboardProvider clipboardProvider;

        public MainWindowViewModelFactory(IClipboardMonitor clipboardMonitor, IClipboardProvider clipboardProvider)
        {
            this.clipboardMonitor = clipboardMonitor;
            this.clipboardProvider = clipboardProvider;
        }

        public MainWindowViewModel Create(IWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            return new MainWindowViewModel(window, clipboardMonitor, clipboardProvider);
        }
    }
}
