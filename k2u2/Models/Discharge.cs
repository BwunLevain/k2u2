using System;
using System.Collections.Generic;

namespace k2u2.Models;

public partial class Discharge
{
    public int DischargeId { get; set; }

    public int FkLoanId { get; set; }

    public DateTime DischargeDateTime { get; set; }

    public virtual Loan FkLoan { get; set; } = null!;
}
