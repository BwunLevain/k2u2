using k2u2.LibraryClient.Models;

namespace k2u2.LibraryClient
{
    public class MenuHandlers
    {
        private readonly MenuService _service;

        public MenuHandlers(MenuService service)
        {
            _service = service;
        }
        public void HandleRegisterBook()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTER NEW BOOK ---");

            long isbn;
            while (true)
            {
                Console.Write("Enter ISBN: ");
                if (long.TryParse(Console.ReadLine(), out isbn)) break;
                Console.WriteLine("Invalid ISBN. Please enter numbers only.");
            }

            Console.Write("Enter Title: ");
            string title = Console.ReadLine() ?? "Unknown Title";

            try
            {
                _service.RegisterBook(isbn, title);
                Console.Clear();
                Console.WriteLine("\nBook registered successfully! Press any key...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase Error: {ex.Message}");
            }
            Console.ReadKey();
        }

        public void HandleRegisterMember()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTER NEW MEMBER ---");

            long pnr;
            while (true)
            {
                Console.Write("Enter Personal Number (YYYYMMDDXXXX): ");
                if (long.TryParse(Console.ReadLine(), out pnr)) break;
                Console.WriteLine("Invalid format. Use numbers only.");
            }

            int pin;
            while (true)
            {
                Console.Write("Enter 4-digit PIN: ");
                if (int.TryParse(Console.ReadLine(), out pin)) break;
                Console.WriteLine("Invalid PIN. Use numbers only.");
            }

            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? "";

            try
            {
                _service.RegisterMember(pnr, pin, email);
                Console.Clear();
                Console.WriteLine("\nMember registered! Press any key...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase Error: {ex.Message}");
            }
            Console.ReadKey();
        }

        public void HandleRegisterLoan()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTER NEW LOAN ---");

            int mId, bId;

            Console.Write("Enter Member ID: ");
            while (!int.TryParse(Console.ReadLine(), out mId)) Console.Write("Invalid ID. Try again: ");

            Console.Write("Enter Book ID: ");
            while (!int.TryParse(Console.ReadLine(), out bId)) Console.Write("Invalid ID. Try again: ");

            try
            {
                _service.RegisterLoan(mId, bId);
                Console.Clear();
                Console.WriteLine("\nLoan processed. Press any key...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            Console.ReadKey();
        }

        public void HandleRegisterReturn()
        {
            Console.Write("Enter Loan ID to return: ");
            int lId = int.Parse(Console.ReadLine() ?? "0");
            _service.RegisterReturn(lId);
            Console.Clear();
            Console.WriteLine("Return processed. Press any key...");
            Console.ReadKey();
        }

        public void HandleViewActiveLoans()
        {
            var loans = _service.GetActiveLoans();
            Console.Clear();
            Console.WriteLine("\n--- ACTIVE LOANS ---");
            foreach (var l in loans)
            {
                Console.WriteLine($"LoanID: {l.LoanId} | Member: {l.MemberEmail} | Title: {l.Title} | Due: {l.DueDate:yyyy-MM-dd}");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public void HandleSearchBooks()
        {
            Console.Clear();
            Console.Write("Search by title or author: ");
            string query = Console.ReadLine() ?? "";

            var results = _service.SearchBooks(query);

            Console.Clear();
            Console.WriteLine($"\n--- SEARCH RESULTS FOR '{query}' ---");

            if (results.Count == 0)
            {
                Console.WriteLine("No books found matching that search term.");
            }
            else
            {
                foreach (var b in results)
                {
                    Console.WriteLine($"ID: {b.BookId,-5} | Title: {b.BookTitle,-30} | ISBN: {b.Isbn}");
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        public void HandleViewAllBooks()
        {
            var books = _service.GetAllBooks();
            Console.Clear();
            Console.WriteLine("\n--- ALL REGISTERED BOOKS ---");
            foreach (var b in books)
            {
                Console.WriteLine($"ID: {b.BookId,-5} | ISBN: {b.Isbn,-13} | Title: {b.BookTitle}");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public void HandleViewAllMembers()
        {
            var members = _service.GetAllMembers();
            Console.Clear();
            Console.WriteLine("\n--- REGISTERED MEMBERS ---");
            foreach (var m in members)
            {
                Console.WriteLine($"ID: {m.LibraryMemberId,-5} | Email: {m.Email,-25} | PNR: {m.PersonalNumber}");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}