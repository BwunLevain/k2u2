using System;
using System.Collections.Generic;

namespace k2u2.LibraryClient.Models;

public partial class VwActiveLoan
{
    public int LoanId { get; set; }

    public string MemberEmail { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime BorrowedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int? DaysOverdue { get; set; }
}
