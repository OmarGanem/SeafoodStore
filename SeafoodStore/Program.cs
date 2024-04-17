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
        /// The main entry method for the application.
        /// Sets up the inventory and processes user commands until the user decides to exit.
        static void Main(string[] args)
        {
            // Initializes the inventory and subscribes to the low stock alert event.
            Inventory inventory = new Inventory();
            inventory.OnLowStock += (message) => Console.WriteLine(message);

            // Controls the main loop of the application.
            bool running = true;
            // Main loop: display options and process user input.
            while (running)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add a new product");
                Console.WriteLine("2. Check stock");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice (1-3): ");
                string choice = Console.ReadLine();

                // Handle user choices
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
            // Prompts the user to press any key to close the console window.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //Prompts the user for product details and adds a new product to the inventory.
        static void AddNewProduct(Inventory inventory)
        {
            // Get product details from the user
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Enter price: ");
            string priceInput = Console.ReadLine();

            // Adds the product to the inventory; uses a default price if no price is entered.
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
