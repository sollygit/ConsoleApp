using System;

namespace ConsoleApp
{
    public class Product
    {
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                string old = this.name;
                this.name = value;
                OnPropertyChange(this, new PropertyChangeEventArgs("Name", old, value));
            }
        }

        public delegate void PropertyChangeHandler(object sender, PropertyChangeEventArgs data);
        public event PropertyChangeHandler PropertyChange;

        // The method which fires the Event
        public void OnPropertyChange(object sender, PropertyChangeEventArgs data)
        {
            // Check if there are any Subscribers
            if (PropertyChange != null)
            {
                // Call the Event
                PropertyChange(this, data);
            }
        }
    }

    public class PropertyChangeEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public PropertyChangeEventArgs(string propertyName, string oldValue, string newValue)
        {
            this.PropertyName = propertyName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
