namespace Shopping_Cart_Project
{
    internal class Program
    {
        static public List<string> cartItems = new List<string>();  // user shopping card
        static public Dictionary<string,double> itemPrices = new Dictionary<string, double>()   // stock
        {
            { "Camera" , 1500 },
            { "Laptop" , 3000 },
            { "TV" , 2500 }
        };
        static Stack<string> action = new Stack<string>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Shopping Cart"); // Welcome Message when open the program
                Console.WriteLine("=================================");
                Console.WriteLine("1. Add Item to Cart");   // Message for The first choice if you want to add item
                Console.WriteLine("2. View Cart Items");    // Message for The Second Choice if the user want to view the items in the basket
                Console.WriteLine("3. Remove Items from Cart"); // Message for The Third Choice when user need to remove item from the basket
                Console.WriteLine("4. Checkout");   // fourth Message for Checking the content of the basket
                Console.WriteLine("5. Undo Last Action");   // fiveth Message for The undo the last action
                Console.WriteLine("6. Exit ");  // sexth Message for Exit from the program


                Console.WriteLine("Enter your choice number : ");
                string choice = Console.ReadLine();
                int intChoice = Convert.ToInt32(choice);

                switch (intChoice)
                {
                    case 1:
                        AddItem();
                        break;
                    case 2:
                        ViewCart();
                        break;
                    case 3:
                        RemoveItem();
                        break;
                    case 4:
                        Checkout();
                        break;
                    case 5:
                        UndoAction();
                        break;
                    case 6:
                        Console.WriteLine("Exiting the application...");
                        Environment.Exit(0); //i add zero to make exit from any warnning
                                             // Environment is a class in the system This will exit the application gracefully
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void AddItem()
        {
            Console.WriteLine("Available Items");
            foreach (var item in itemPrices)
            {
                Console.WriteLine($"Item : {item.Key}  Price : {item.Value}");
            }
            Console.WriteLine("Please Enter Product Name");
            string cartItem = Console.ReadLine();

            if (itemPrices.ContainsKey(cartItem))
            {
                cartItems.Add(cartItem);
                action.Push($"Item {cartItem} is added to your cart");
                Console.WriteLine($"Item {cartItems} is added to your cart");
            }
            else
            {
                Console.WriteLine("Item is Out of Stock or not Available");
            }
        }

        private static void ViewCart()
        {
            Console.WriteLine("Your Card Items : ");
            if (cartItems.Any()) // check if found item or not 
            {
                var itemPriceCollection = GetCartPrices();

                foreach (var item in itemPriceCollection)
                {
                    Console.WriteLine($"Item : {item.Item1} Price : {item.Item2}");
                }
            }
            else
            {
                Console.WriteLine("Cart is Empty.");
            }
            
        }
        
        private static IEnumerable<Tuple<string, double>> GetCartPrices()
        {
            var cartprices = new List<Tuple<string, double>>();
            foreach (var item in cartItems) 
            {
                double price = 0;
                bool foundItem = itemPrices.TryGetValue(item, out price);
                if (foundItem)
                {
                    Tuple<string, double> itemPrice = new Tuple<string, double>(item, price);
                    cartprices.Add(itemPrice);
                }
            }
            return cartprices;
        }
        private static void RemoveItem()
        {
            ViewCart();
            if (cartItems.Any())
            {
                Console.WriteLine("Please Select Item to Remove !");
                string itemToRemove = Console.ReadLine();
                if (cartItems.Contains(itemToRemove))
                {
                    cartItems.Remove(itemToRemove);
                    action.Push($"Item {itemToRemove} removed from your cart");
                    Console.WriteLine($"Items: {itemToRemove} removed");
                }
                else
                {
                    Console.WriteLine("Item doesn't exist in shop!");
                }
            }
        }

        private static void Checkout()
        {
            if (cartItems.Any()) 
            {
                double totalPrice = 0;
                Console.WriteLine($"Your cart items are: ");

                IEnumerable<Tuple<string, double>> itemsinCart = GetCartPrices();

                foreach (var item in itemsinCart) 
                {
                    totalPrice = totalPrice + item.Item2;
                    Console.WriteLine(item.Item1 + " " + item.Item2);
                }
                Console.WriteLine($"Total Price to pay: {totalPrice}");
                Console.WriteLine("Please proceed to payment, Thank you for shopping with!");

                cartItems.Clear();
                action.Push("Checkout ");
            }
            else
            {
                 Console.WriteLine("Your cart is empty!"); 
            }
        }

        private static void UndoAction()
        {
            if (action.Count() > 0)
            {
                string lastAction = action.Pop();
                Console.WriteLine($"Your last action is {lastAction}");

                var actionArray = lastAction.Split();

                if (lastAction.Contains("added"))
                {
                    cartItems.Remove(actionArray[1]);
                }
                else if (lastAction.Contains("removed"))
                {
                    cartItems.Add(actionArray[1]);
                }
                else
                {
                    Console.WriteLine("Check out cannot be undo, please ask for refund!");
                }
            }
        }
    }
}
