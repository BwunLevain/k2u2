namespace k2u2.LibraryClient
{
    public class MenuUI
    {
        private readonly MenuHandlers _handlers;

        public MenuUI(MenuHandlers handlers)
        {
            _handlers = handlers;
        }

        public void ShowMainMenu()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.Clear();
                Console.WriteLine("======= LIBRARY SYSTEM MENU =======");
                Console.WriteLine("1. Register new books");
                Console.WriteLine("2. Register new members");
                Console.WriteLine("3. Register loans");
                Console.WriteLine("4. Register returns");
                Console.WriteLine("5. View all active loans");
                Console.WriteLine("6. Search for books");
                Console.WriteLine("7. View all books");
                Console.WriteLine("8. View all members");
                Console.WriteLine("0. Exit");
                Console.WriteLine("===================================");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": _handlers.HandleRegisterBook(); break;
                    case "2": _handlers.HandleRegisterMember(); break;
                    case "3": _handlers.HandleRegisterLoan(); break;
                    case "4": _handlers.HandleRegisterReturn(); break;
                    case "5": _handlers.HandleViewActiveLoans(); break;
                    case "6": _handlers.HandleSearchBooks(); break;
                    case "7": _handlers.HandleViewAllBooks(); break;
                    case "8": _handlers.HandleViewAllMembers(); break;
                    case "0": continueRunning = false; break;
                    default:
                        Console.WriteLine("Invalid selection. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}