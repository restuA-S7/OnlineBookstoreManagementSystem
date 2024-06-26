using System;
using System.Collections.Generic;

namespace BookstoreAPP.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookId { get; set; }

    public int CustomerId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
