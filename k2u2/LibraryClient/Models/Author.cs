using System;
using System.Collections.Generic;

namespace k2u2.LibraryClient.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string AuthorFirstName { get; set; } = null!;

    public string AuthorLastName { get; set; } = null!;

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}
