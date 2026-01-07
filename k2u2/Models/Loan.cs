using System;
using System.Collections.Generic;

namespace k2u2.Models;

public partial class Loan
{
    public int LoanId { get; set; }

    public int FkLibraryMemberId { get; set; }

    public int FkBookId { get; set; }

    public DateTime LoanDateTime { get; set; }

    public int LoanPeriod { get; set; }

    public virtual Discharge? Discharge { get; set; }

    public virtual Book FkBook { get; set; } = null!;

    public virtual LibraryMember FkLibraryMember { get; set; } = null!;
}
