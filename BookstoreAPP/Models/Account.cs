using System;
using System.Collections.Generic;

namespace BookstoreAPP.Models;

public partial class Account
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Customer User { get; set; } = null!;
}
