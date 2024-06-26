﻿using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace WorkerApp.Models;

public partial class BookstoreDbContext : DbContext
{
    public BookstoreDbContext()
    {
    }

    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=127.0.0.1,1455;database=BookstoreDb;uid=sa;pwd=Password#123;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Books");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Books");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    //public void SeedData()
    //{
    //    var faker = new Faker();

    //    // Generate fake customers

    //    var customers = new Faker<Customer>()
    //        .RuleFor(c => c.Name, f => f.Name.FullName())
    //        .RuleFor(c => c.Email, f => f.Internet.Email())
    //        .Generate(10);

    //    Customers.AddRange(customers);

    //    // Generate fake books
    //    var books = new Faker<Book>()
    //        .RuleFor(b => b.Title, f => f.Commerce.ProductName())
    //        .RuleFor(b => b.Author, f => f.Name.FullName())
    //        .RuleFor(b => b.Price, f => f.Commerce.Price())
    //        .Generate(10);

    //    Books.AddRange(books);

    //    // Save changes to generate IDs
    //    SaveChanges();

    //    // Generate fake orders
    //    var orders = new Faker<Order>()
    //        .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).CustomerId)
    //        .RuleFor(o => o.OrderDate, f => f.Date.Past())
    //        .RuleFor(o => o.TotalAmount, f => f.Commerce.Price())
    //        .Generate(5);

    //    Orders.AddRange(orders);

    //    // Save changes to generate IDs
    //    SaveChanges();

    //    // Generate fake order details
    //    var orderDetails = new Faker<OrderDetail>()
    //        .RuleFor(od => od.OrderId, f => f.PickRandom(orders).OrderId)
    //        .RuleFor(od => od.BookId, f => f.PickRandom(books).BookId)
    //        .RuleFor(od => od.Price, f => f.Commerce.Price())
    //        .Generate(20);

    //    OrderDetails.AddRange(orderDetails);

    //    // Generate fake reviews
    //    var reviews = new Faker<Review>()
    //        .RuleFor(r => r.BookId, f => f.PickRandom(books).BookId)
    //        .RuleFor(r => r.CustomerId, f => f.PickRandom(customers).CustomerId)
    //        .RuleFor(r => r.ReviewText, f => f.Lorem.Paragraph())
    //        .Generate(15);

    //    Reviews.AddRange(reviews);

    //    // Save all changes
    //    SaveChanges();
    //}
}
