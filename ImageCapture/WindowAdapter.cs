﻿using System;
using System.Windows;
using System.Windows.Input;

namespace ImageCapture
{
    public class WindowAdapter : IWindow
    {
        private readonly Window wpfWindow;

        public WindowAdapter(Window wpfWindow)
        {
            if (wpfWindow == null)
            {
                throw new ArgumentNullException("window");
            }

            this.wpfWindow = wpfWindow;
        }

        #region IWindow Members

        public virtual void Close()
        {
            this.wpfWindow.Close();
        }

        public virtual IWindow CreateChild(object viewModel)
        {
            var cw = new ContentWindow();
            cw.Owner = this.wpfWindow;
            cw.DataContext = viewModel;
            WindowAdapter.ConfigureBehavior(cw);

            return new WindowAdapter(cw);
        }

        public virtual void Show()
        {
            this.wpfWindow.Show();
        }

        public virtual bool? ShowDialog()
        {
            return this.wpfWindow.ShowDialog();
        }

        #endregion

        protected Window WpfWindow
        {
            get { return this.wpfWindow; }
        }

        private static void ConfigureBehavior(ContentWindow cw)
        {
            cw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            cw.CommandBindings.Add(new CommandBinding(PresentationCommands.Accept, (sender, e) => cw.DialogResult = true));
        }
    }
}
