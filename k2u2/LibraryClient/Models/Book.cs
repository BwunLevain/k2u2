using System;
using System.Collections.Generic;

namespace k2u2.LibraryClient.Models;

public partial class Book
{
    public int BookId { get; set; }

    public long Isbn { get; set; }

    public string BookTitle { get; set; } = null!;

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
