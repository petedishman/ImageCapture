using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageCapture
{
    class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClipboardProvider>().To<ClipboardProvider>();
            Bind<IClipboardMonitor>().To<ClipboardMonitor>();
            Bind<IMainWindowViewModelFactory>().To<MainWindowViewModelFactory>();

            Bind<Window>().To<MainWindow>();
            Bind<IWindow>().To<MainWindowAdapter>().InTransientScope();
        }
    }
}
