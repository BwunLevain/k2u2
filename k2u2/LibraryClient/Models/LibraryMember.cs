using System;
using System.Collections.Generic;

namespace k2u2.LibraryClient.Models;

public partial class LibraryMember
{
    public int LibraryMemberId { get; set; }

    public long PersonalNumber { get; set; }

    public int Pincode { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
