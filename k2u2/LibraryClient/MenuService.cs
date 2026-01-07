using k2u2.LibraryClient.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace k2u2.LibraryClient
{
    public class MenuService
    {
        private readonly LibraryDbContext _context;

        public MenuService(LibraryDbContext context)
        {
            _context = context;
        }

        public void RegisterBook(long isbn, string title)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_RegisterBook @ISBN={0}, @Title={1}", isbn, title);
        }

        public void RegisterMember(long pnr, int pin, string email)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_RegisterLibraryMember @PersonalNumber={0}, @PINCode={1}, @Email={2}", pnr, pin, email);
        }

        public void RegisterLoan(int memberId, int bookId)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_LoanBook @MemberId={0}, @BookId={1}, @LoanPeriod=14", memberId, bookId);
        }

        public void RegisterReturn(int loanId)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_ReturnBook @LoanId={0}", loanId);
        }

        public List<VwActiveLoan> GetActiveLoans()
        {
            return _context.VwActiveLoans.ToList();
        }

        public List<Book> SearchBooks(string query)
        {
            string fuzzyQuery = $"%{query}%";

            return _context.Books
                .FromSqlRaw("EXEC sp_SearchBooks @SearchTitle={0}, @SearchAuthor={0}", fuzzyQuery)
                .ToList();
        }
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public List<LibraryMember> GetAllMembers()
        {
            return _context.LibraryMembers.ToList();
        }
    }
}