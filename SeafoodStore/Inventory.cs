using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SeafoodStore
{
    /// Manages the inventory of seafood items including adding items, checking stock levels,
    /// serializing inventory data to a file, and deserializing data from a file.
    public class Inventory
    {
        /// Gets the list of seafood items currently in inventory.
        public List<SeafoodItem> Items { get; private set; } = new List<SeafoodItem>();
        /// Delegate for handling notifications about low stock levels.
        public delegate void StockAlert(string message);
        /// Event triggered when the stock of any seafood item falls below a specified threshold.
        public event StockAlert OnLowStock;
        
        /// Adds a new item to the inventory or updates the quantity of an existing item.
        /// Triggers a stock check to notify if stock levels are below the threshold.
        public void AddItem(string name, int quantity, double price = 10.0)
        {
            SeafoodItem item = Items.Find(i => i.Name == name);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new SeafoodItem(name, price, quantity);
                Items.Add(item);
            }
            // Ensure the item is not null before checking stock
            if (item != null)
            {
                CheckStock(item);
            }
        }

        /// Checks the stock level of a specific item and triggers the OnLowStock event
        /// if the quantity is below the threshold (30 units).
        private void CheckStock(SeafoodItem item)
        {
            // Check if item is null before accessing its properties
            if (item != null && item.Quantity < 30)
            {
                OnLowStock?.Invoke($"Warning: Low stock on {item.Name}. Only {item.Quantity} left.");
            }
        }

        /// Serializes the current inventory to a file.
        public void SerializeInventory(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, Items);
            }
        }

        /// Deserializes the inventory from a file.
        public void DeserializeInventory(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                Items = (List<SeafoodItem>)formatter.Deserialize(stream);
            }
        }

        /// Displays the current inventory to the console.
        public void DisplayInventory()
        {
            Console.WriteLine("Current Inventory:");
            foreach (SeafoodItem item in Items)
            {
                Console.WriteLine($"Name: {item.Name}, Price: ${item.Price}, Quantity: {item.Quantity}");
            }
        }
        /// Updates the specified attributes of an existing item in the inventory.
        public void UpdateItem(string name, double? newPrice = null, int? newQuantity = null)
        /// name="name">The name of the item to update. This is used to identify the item in the inventory
        /// name="newPrice". The new price to set for the item. If null, the price remains unchanged
        /// name="newQuantity". The new quantity to set for the item. If null, the quantity remains unchanged
        {
            SeafoodItem item = Items.Find(i => i.Name == name);
            if (item != null)
            /// This method checks if the item exists in the inventory based on the name.
            {
                if (newPrice.HasValue)
                {
                    item.Price = newPrice.Value;
                }
                if (newQuantity.HasValue)
                {
                    item.Quantity = newQuantity.Value;
                    CheckStock(item);  // Optionally check stock after updating
                }
                Console.WriteLine($"Item updated: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
                /// If the item exists, it updates the specified attributes and checks the stock level, potentially triggering a low stock alert.
            }
            else
            {
                Console.WriteLine($"Item with name {name} not found."); /// If the item does not exist, it outputs a message indicating that the item was not found.
            }
        }
        /// Removes an item from the inventory based on its name.
        
        
        public void DeleteItem(string name)
        /// name="name" The name of the item to be removed. This is used to identify the item in the inventory
        {
            SeafoodItem item = Items.Find(i => i.Name == name);
            if (item != null)
            /// This method checks if the item exists in the inventory based on the name.
            {
                Items.Remove(item);
                Console.WriteLine($"Item removed: {name}");
                /// If the item exists, it is removed from the inventory and a confirmation message is displayed.
            }
            else
            {
                Console.WriteLine($"Item with name {name} not found.");
                /// If the item does not exist, a message indicating that the item was not found is displayed.
            }
        }
    }

}
