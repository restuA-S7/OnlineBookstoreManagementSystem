using System;
using System.Linq;
using ManagementApp.Models;

namespace ManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu Utama:");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. Update Data");
                Console.WriteLine("3. Delete Data");
                Console.WriteLine("4. Lihat Inventaris");
                Console.WriteLine("5. Exit");
                Console.Write("Pilih opsi: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            TambahBukuBaru();
                            break;
                        case 2:
                            PerbaruiBuku();
                            break;
                        case 3:
                            HapusBuku();
                            break;
                        case 4:
                            LihatInventaris();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Opsi tidak valid. Tekan sembarang tombol untuk mencoba lagi.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input tidak valid. Tekan sembarang tombol untuk mencoba lagi.");
                    Console.ReadKey();
                }
            }
        }

        static void TambahBukuBaru()
        {
            using (var context = new BookstoreDbContext())
            {
                Console.Clear();
                Book newBook = new Book();
                Console.Write("Masukkan Judul Buku: ");
                newBook.Title = Console.ReadLine();
                Console.Write("Masukkan Penulis Buku: ");
                newBook.Author = Console.ReadLine();
                Console.Write("Masukkan Harga Buku: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    newBook.Price = price;
                }
                else
                {
                    Console.WriteLine("Harga tidak valid. Tekan sembarang tombol untuk kembali ke menu utama.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Masukkan Jumlah Stok: ");
                if (int.TryParse(Console.ReadLine(), out int stock))
                {
                    newBook.Stock = stock;
                }
                else
                {
                    Console.WriteLine("Jumlah stok tidak valid. Tekan sembarang tombol untuk kembali ke menu utama.");
                    Console.ReadKey();
                    return;
                }
                context.Books.Add(newBook);
                context.SaveChanges();
                Console.WriteLine("Buku berhasil ditambahkan! Tekan sembarang tombol untuk kembali ke menu utama.");
                Console.ReadKey();
            }
        }

        static void PerbaruiBuku()
        {
            using (var context = new BookstoreDbContext())
            {
                Console.Clear();
                Console.Write("Masukkan ID Buku yang ingin diperbarui: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Book book = context.Books.Find(bookId);
                    if (book != null)
                    {
                        Console.Write("Masukkan Judul Buku baru (kosongkan untuk tidak mengubah): ");
                        string title = Console.ReadLine();
                        if (!string.IsNullOrEmpty(title))
                        {
                            book.Title = title;
                        }
                        Console.Write("Masukkan Penulis Buku baru (kosongkan untuk tidak mengubah): ");
                        string author = Console.ReadLine();
                        if (!string.IsNullOrEmpty(author))
                        {
                            book.Author = author;
                        }
                        Console.Write("Masukkan Harga Buku baru (kosongkan untuk tidak mengubah): ");
                        string priceInput = Console.ReadLine();
                        if (decimal.TryParse(priceInput, out decimal price))
                        {
                            book.Price = price;
                        }
                        Console.Write("Masukkan Jumlah Stok baru (kosongkan untuk tidak mengubah): ");
                        string stockInput = Console.ReadLine();
                        if (int.TryParse(stockInput, out int stock))
                        {
                            book.Stock = stock;
                        }
                        context.SaveChanges();
                        Console.WriteLine("Buku berhasil diperbarui! Tekan sembarang tombol untuk kembali ke menu utama.");
                    }
                    else
                    {
                        Console.WriteLine("Buku tidak ditemukan. Tekan sembarang tombol untuk kembali ke menu utama.");
                    }
                }
                else
                {
                    Console.WriteLine("Input tidak valid. Tekan sembarang tombol untuk kembali ke menu utama.");
                }
                Console.ReadKey();
            }
        }

        static void HapusBuku()
        {
            using (var context = new BookstoreDbContext())
            {
                Console.Clear();
                Console.Write("Masukkan ID Buku yang ingin dihapus: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Book book = context.Books.Find(bookId);
                    if (book != null)
                    {
                        Console.Write($"Yakin ingin menghapus buku dengan Judul: {book.Title} (y/n)? ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            context.Books.Remove(book);
                            context.SaveChanges();
                            Console.WriteLine("Buku berhasil dihapus! Tekan sembarang tombol untuk kembali ke menu utama.");
                        }
                        else
                        {
                            Console.WriteLine("Penghapusan dibatalkan. Tekan sembarang tombol untuk kembali ke menu utama.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Buku tidak ditemukan. Tekan sembarang tombol untuk kembali ke menu utama.");
                    }
                }
                else
                {
                    Console.WriteLine("Input tidak valid. Tekan sembarang tombol untuk kembali ke menu utama.");
                }
                Console.ReadKey();
            }
        }

        static void LihatInventaris()
        {
            using (var context = new BookstoreDbContext())
            {
                Console.Clear();
                Console.WriteLine("Inventaris Buku:");
                Console.WriteLine("ID\tJudul\tPenulis\tHarga\tStok");
                foreach (var book in context.Books)
                {
                    Console.WriteLine($"{book.BookId}\t{book.Title}\t{book.Author}\t{book.Price}\t{book.Stock}");
                }
                Console.WriteLine("Tekan sembarang tombol untuk kembali ke menu utama.");
                Console.ReadKey();
            }
        }
    }
}
