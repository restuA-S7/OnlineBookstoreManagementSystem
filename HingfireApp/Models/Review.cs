﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HangfireApp.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookId { get; set; }

    public int CustomerId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public virtual Book Book { get; set; }

    public virtual Customer Customer { get; set; }
}