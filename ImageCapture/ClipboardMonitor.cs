using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ImageCapture
{
    public interface IClipboardMonitor
    {
        event EventHandler ClipboardUpdate;
    }

    /// <summary>
    /// Monitors the clipboard for new content and fires the event ClipboardUpdate
    /// when the clipboard contains an image
    /// </summary>
    public class ClipboardMonitor : IClipboardMonitor
    {
        public event EventHandler ClipboardUpdate;

        private Timer timer;

        public ClipboardMonitor()
        {
            // In order to get clipboard notification messages we need to hook in to the Windows Message loop.
            // To do that we need the window handle, which won't exist when we're created.
            // So, we'll actually setup a timer and keep checking for it until it exists.
            // no one will notice
            if (IsMainWindowHandleAvailable())
            {
                Debug.WriteLine("Main Window Handle Available initially");
                SetupClipboardListener();
            }
            else
            {
                timer = new Timer(CheckIfWindowHandleAvailable, this, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(-1));
            }            
        }

        private static void CheckIfWindowHandleAvailable(object state)
        {
            Debug.WriteLine("Checking for window handle");
            // need to access the window handle stuff from the right thread
            Application.Current.Dispatcher.Invoke(() => {
                ClipboardMonitor monitor = state as ClipboardMonitor;
                if (monitor != null)
                {
                    if (monitor.IsMainWindowHandleAvailable())
                    {
                        monitor.timer.Dispose();
                        monitor.SetupClipboardListener();
                    }
                }
            });

        }

        private bool IsMainWindowHandleAvailable()
        {
            var windowHelper = new WindowInteropHelper(Application.Current.MainWindow);
            return windowHelper.Handle != IntPtr.Zero;
        }

        private void SetupClipboardListener()
        {
            var windowHelper = new WindowInteropHelper(Application.Current.MainWindow);
            var source = HwndSource.FromHwnd(windowHelper.Handle);
            source.AddHook(WndProc);

            if (!SafeNativeMethods.AddClipboardFormatListener(source.Handle))
            {
                // failed 
                Debug.WriteLine("Call to AddClipboardFormatListener failed, won't receieve clipboard notifications");
            }
        }

        [DebuggerStepThrough]
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == SafeNativeMethods.WM_CLIPBOARDUPDATE)
            {
                OnClipboardChanged();
            }

            return IntPtr.Zero;
        }

        private void OnClipboardChanged()
        {
            if (Clipboard.ContainsImage() && ClipboardUpdate != null)
            {
                ClipboardUpdate(null, null);
            }
        }
    }
}
