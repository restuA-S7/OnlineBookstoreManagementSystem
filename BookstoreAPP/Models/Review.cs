﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BookstoreAPP.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookId { get; set; }

    public int CustomerId { get; set; }

    public int Rating { get; set; } = 0;

    public string Comment { get; set; } = null;

    public Book Book { get; set; }

    public Customer Customer { get; set; }
}