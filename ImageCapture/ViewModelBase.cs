using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace ImageCapture
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;

            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private readonly string ERROR_MSG = "The class {1} does not have a public property '{0}'";

        [Conditional("DEBUG"), DebuggerStepThrough()]
        public void VerifyPropertyName(string propertyName)
        {
            Type type = this.GetType();

            PropertyInfo propInfo = type.GetProperty(propertyName);

            if (propInfo == null)
            {
                // property not found, break in to debugger
                string msg = string.Format(ERROR_MSG, propertyName, type.FullName);

                Debug.Fail(msg);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
