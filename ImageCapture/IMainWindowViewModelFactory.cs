using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCapture
{
    interface IMainWindowViewModelFactory
    {
        MainWindowViewModel Create(IWindow window);
    }
}
