using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageCapture
{
    public interface IClipboardProvider
    {
        bool ContainsImage { get; }
        Image GetImage();
    }

    public class ClipboardProvider : IClipboardProvider
    {
        public bool ContainsImage
        {
            get
            {
                return Clipboard.ContainsImage();
            }
        }

        public Image GetImage()
        {
            if (!Clipboard.ContainsImage())
            {
                return null;
            }

            return new Image(Clipboard.GetImage());
        }
    }
}
