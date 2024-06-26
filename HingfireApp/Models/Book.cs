﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HangfireApp.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}