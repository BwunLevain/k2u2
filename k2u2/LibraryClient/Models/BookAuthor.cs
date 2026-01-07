using System;
using System.Collections.Generic;

namespace k2u2.LibraryClient.Models;

public partial class BookAuthor
{
    public int BookAuthorId { get; set; }

    public int FkBookId { get; set; }

    public int FkAuthorId { get; set; }

    public virtual Author FkAuthor { get; set; } = null!;

    public virtual Book FkBook { get; set; } = null!;
}
