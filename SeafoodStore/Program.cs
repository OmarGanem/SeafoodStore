using SeafoodStore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Add a new product");
                Console.WriteLine("2. Update an existing product");
                Console.WriteLine("3. Delete a product");
                Console.WriteLine("4. Check stock");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");
                string choice = Console.ReadLine();

                // Handle user choices
                switch (choice)
                {
                    case "1":
                        AddNewProduct(inventory);
                        break;
                    case "2":
                        UpdateProduct(inventory);
                        break;
                    case "3":
                        DeleteProduct(inventory);
                        break;
                    case "4":
                        inventory.DisplayInventory();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please select 1, 2, 3, 4, or 5.");
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
        /// Updates an existing product in the inventory by prompting the user for input.
        static void UpdateProduct(Inventory inventory)
        /// name="inventory" The inventory object where the products are stored.
        {
            Console.Write("Enter product name to update: ");
            string name = Console.ReadLine();
            /// This method prompts the user for the product name, the new price, and the new quantity.
            

            Console.Write("Enter new price (or leave blank to keep current price): ");
            string priceInput = Console.ReadLine();
            double? price = null;
            if (!string.IsNullOrWhiteSpace(priceInput))
            {
                if (double.TryParse(priceInput, out double parsedPrice))
                {
                    price = parsedPrice;
                }
                else
                {
                    Console.WriteLine("Invalid input for price. Keeping the current price.");
                    
                }
            }
            /// It performs validation checks on the input for price and quantity. If the inputs are valid, it updates the product.

            Console.Write("Enter new quantity (or leave blank to keep current quantity): ");
            string quantityInput = Console.ReadLine();
            int? quantity = null;
            if (!string.IsNullOrWhiteSpace(quantityInput))
            {
                if (int.TryParse(quantityInput, out int parsedQuantity))
                {
                    quantity = parsedQuantity;
                }
                else
                {
                    Console.WriteLine("Invalid input for quantity. Keeping the current quantity.");
                }
            }
            inventory.UpdateItem(name, price, quantity);
            Console.WriteLine("Product update attempted.");
            /// If inputs are invalid, it retains the original values and notifies the user.
        }

        /// Deletes a product from the inventory by prompting the user for the product name.
        static void DeleteProduct(Inventory inventory)
        /// name="inventory" The inventory object from which the product will be deleted.
        {
            Console.Write("Enter product name to delete: ");
            /// This method prompts the user for the name of the product to be deleted.
            /// It then attempts to delete the product from the inventory.
            string name = Console.ReadLine();
            inventory.DeleteItem(name);
            Console.WriteLine("Deletion attempted.");
        }
    }
}
