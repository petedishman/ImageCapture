using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageCapture
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IWindow window;

        private IClipboardMonitor clipboardMonitor;
        private IClipboardProvider clipboardProvider;

        public MainWindowViewModel(IWindow window, IClipboardMonitor clipboardMonitor, IClipboardProvider clipboardProvider)
        {
            this.window = window;
            this.clipboardMonitor = clipboardMonitor;
            this.clipboardProvider = clipboardProvider;

            this.clipboardMonitor.ClipboardUpdate += OnClipboardUpdate;

            OnClipboardUpdate(null, null);

            SaveCommand = new RelayCommand(ExecuteSaveCommand);
        }

        private void OnClipboardUpdate(object sender, EventArgs e)
        {
            if (clipboardProvider.ContainsImage)
            {
                var image = clipboardProvider.GetImage();
                Image = image.Data;
                Width = image.Width;
                Height = image.Height;
            }
        }

        private void ExecuteSaveCommand(object obj)
        {
            if (_Image != null)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "screenshot"; // Default file name
                dlg.DefaultExt = ".png"; // Default file extension
                dlg.Filter = "Images (.png)|*.png"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    using (var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(_Image));
                        encoder.Save(fileStream);
                    }
                }
            }
        }

        public RelayCommand SaveCommand { get; private set; }


        private BitmapSource _Image;
        public BitmapSource Image
        {
            get
            {
                return _Image;
            }
            set
            {
                var propertyChanged = (_Image != value);
                _Image = value;
                if (propertyChanged)
                {
                    RaisePropertyChanged("Image");
                }
            }
        }

        private int _Width;
        public int Width
        {
            get { return _Width; }
            set
            {
                var propertyChanged = (_Width != value);
                if (propertyChanged)
                {
                    _Width = value;
                    RaisePropertyChanged("Width");
                }
            }
        }
        private int _Height;
        public int Height
        {
            get { return _Height; }
            set
            {
                var propertyChanged = (_Height != value);
                if (propertyChanged)
                {
                    _Height = value;
                    RaisePropertyChanged("Height");
                }
            }
        }
    }
}
