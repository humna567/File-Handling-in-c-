using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileHandlingDemo
{
    class Book
    {
        public string Title;
        public string Author;
        public decimal Price;

        public Book(string title, string author, decimal price)
        {
            Title = title;
            Author = author;
            Price = price;
        }
    }

    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------------Humna's Virtual Library :-) -----------------------------------------");
            LoadBooksFromFile();

            while (true)
            {
               
               
                Console.ResetColor();

                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. Search Books");
                Console.WriteLine("3. List All Books");
                Console.WriteLine("4. Update a Book");
                Console.WriteLine("5. Delete a Book");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddBook();
                        break;
                    case 2:
                        SearchBooks();
                        break;
                    case 3:
                        ListAllBooks();
                        break;
                    case 4:
                        UpdateBook();
                        break;
                    case 5:
                        DeleteBook();
                        break;
                    case 6:
                        SaveBooksToFile();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Exiting the program.");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine();
            }
        }



        static void LoadBooksFromFile()
        {
            if (File.Exists("D:\\data.txt")) // my file is in D directory
            {
                string[] lines = File.ReadAllLines("D:\\data.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(','); // file ma jo sav krygi unko commas ma 

                    if (parts.Length >= 3) // title , authora , price 
                    {
                        string title = parts[0];
                        string author = parts[1];

                        if (decimal.TryParse(parts[2], out decimal price))  // ye parse krky decimal mai change krha . this is an effective way becoz ye exception free hoga. if decimal kay elwa any this=g else di to error 
                        {
                            books.Add(new Book(title, author, price));
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid price: {parts[2]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Skipping invalid line: {line}");
                    }
                }
            }
        }
   

        static void SaveBooksToFile()
        {
            List<string> lines = books.Select(b => $"{b.Title},{b.Author},{b.Price}").ToList();  // aik array ma save krha lists ko 
            File.WriteAllLines("D:\\data.txt", lines);
        }

        static void AddBook()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());  
            Book newBook = new Book(title, author, price);
            books.Add(newBook);

            Console.WriteLine("Book added successfully.");
        }

        static void SearchBooks()
        {
            Console.Write("Enter keyword to search: ");
            string keyword = Console.ReadLine();

            var searchResults = books.Where(b => b.Title.Contains(keyword) || b.Author.Contains(keyword) || b.Price.ToString().Contains(keyword)).ToList();//we use lambda exp  // agar ham ToList nh karygy to result show nh hoga. becoz TOlit convert the enteries in Array.

            if (searchResults.Count > 0)
            {
                Console.WriteLine("Search results:");
                foreach (var book in searchResults)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Price: {book.Price}");  
                }
            }
            else
            {
                Console.WriteLine("No matching books found.");
            }
        }

        static void ListAllBooks()
        {
            if (books.Count > 0)
            {
                Console.WriteLine("List of all books:");
                foreach (var book in books)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Price: {book.Price}");  
                }
            }
            else
            {
                Console.WriteLine("No books found.");
            }
        }

        static void DeleteBook()
        {
            Console.Write("Enter the title of the book to delete: ");  
            string titleToDelete = Console.ReadLine();

            Book bookToDelete = books.FirstOrDefault(b => b.Title == titleToDelete);   //lambda expression // jab usko title miljyga then delete.

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book with the given title not found.");  
            }
        }
        static void UpdateBook()
        {
            Console.Write("Enter the title of the book to update: ");
            string titleToUpdate = Console.ReadLine();

            Book bookToUpdate = books.FirstOrDefault(b => b.Title == titleToUpdate);

            if (bookToUpdate != null)
            {
                Console.Write("Enter new Title: ");
                string newTitle = Console.ReadLine();

                Console.Write("Enter new Author: ");
                string newAuthor = Console.ReadLine();

                Console.Write("Enter new Price: ");
                decimal newPrice = Convert.ToDecimal(Console.ReadLine());

                bookToUpdate.Title = newTitle;
                bookToUpdate.Author = newAuthor;
                bookToUpdate.Price = newPrice;

                Console.WriteLine("Book updated successfully.");
            }
            else
            {
                Console.WriteLine("Book with the given title not found.");
            }
        }
    }
}
    

