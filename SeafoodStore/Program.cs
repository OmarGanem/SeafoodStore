using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodStore
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            inventory.OnLowStock += (message) => Console.WriteLine(message);

            bool running = true;
            while (running)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add a new product");
                Console.WriteLine("2. Check stock");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice (1-3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewProduct(inventory);
                        break;
                    case "2":
                        inventory.DisplayInventory();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please select 1, 2, or 3.");
                        break;
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void AddNewProduct(Inventory inventory)
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Enter price");
            string priceInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(priceInput))
            {
                inventory.AddItem(name, quantity);
            }
            else
            {
                double price = double.Parse(priceInput);
                inventory.AddItem(name, quantity, price);
            }
        }
    }


}
