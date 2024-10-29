using System;

namespace ConsoleApp.Models
{
    public class PropertyChangeEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public PropertyChangeEventArgs(string propertyName, string oldValue, string newValue)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
