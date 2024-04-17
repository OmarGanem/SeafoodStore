using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodStore
{
    /// Represents an individual item in the seafood store's inventory.
    /// This class is marked serializable to allow inventory data to be easily saved to and loaded from a file.
    [Serializable]
    public class SeafoodItem
    {
        /// Gets or sets the name of the seafood item.
        public string Name { get; set; }

        /// Gets or sets the price of the seafood item.
        public double Price { get; set; }

        /// Gets or sets the quantity of the seafood item in stock.
        public int Quantity { get; set; }

        /// Initializes a new instance of the SeafoodItem class with specified details.

        public SeafoodItem(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

}
