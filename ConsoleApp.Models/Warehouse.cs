using System;

namespace ConsoleApp.Models
{
    public class Product
    {
        string productName;

        public int ProductId { get; set; }
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                string old = productName;
                productName = value;

                OnPropertyChange(this, new PropertyChangeEventArgs("ProductName", old, value));
            }
        }

        public delegate void PropertyChangeHandler(object sender, PropertyChangeEventArgs data);
        public event PropertyChangeHandler PropertyChange;

        // The method which fires the Event
        public void OnPropertyChange(object sender, PropertyChangeEventArgs data)
        {
            // Ensure to have any Subscribers and invoke the Event
            PropertyChange?.Invoke(this, data);
        }
    }

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

    public class RetailerProduct
    {
        public int ProductId { get; set; }
        public string RetailerName { get; set; }
        public string RetailerProductCode { get; set; }
        public string RetailerProductCodeType { get; set; }
        public DateTime DateReceived { get; set; }
    }

    public class OutputProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CodeType { get; set; }
        public string Code { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
