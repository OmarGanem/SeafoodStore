using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SeafoodStore
{
    public class Inventory
    {
        public List<SeafoodItem> Items { get; private set; } = new List<SeafoodItem>();
        public delegate void StockAlert(string message);
        public event StockAlert OnLowStock;

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

        private void CheckStock(SeafoodItem item)
        {
            // Check if item is null before accessing its properties
            if (item != null && item.Quantity < 30)
            {
                OnLowStock?.Invoke($"Warning: Low stock on {item.Name}. Only {item.Quantity} left.");
            }
        }

        public void SerializeInventory(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, Items);
            }
        }

        public void DeserializeInventory(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                Items = (List<SeafoodItem>)formatter.Deserialize(stream);
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Current Inventory:");
            foreach (SeafoodItem item in Items)
            {
                Console.WriteLine($"Name: {item.Name}, Price: ${item.Price}, Quantity: {item.Quantity}");
            }
        }
    }

}
