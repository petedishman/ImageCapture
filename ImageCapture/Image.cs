using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageCapture
{
    public class Image
    {
        public BitmapSource Data {get; private set;}
        public int Width { get; private set; }
        public int Height { get; private set; }
        public DateTime Created { get; private set; }

        public Image(BitmapSource data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.Data = data;
            Width = data.PixelWidth;
            Height = data.PixelHeight;
            Created = DateTime.Now;
        }
    }
}
