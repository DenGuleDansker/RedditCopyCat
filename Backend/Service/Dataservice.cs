using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Model;

namespace Service;

public class DataService
{
    private TopicContext db { get; }

    public DataService(TopicContext db)
    {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>

    //public List<Book> GetBooks()
    //{
    //    return db.Books.Include(b => b.Author).ToList();
    //}

    //public Book GetBook(int id)
    //{
    //    return db.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == id);
    //}

    //public List<Author> GetAuthors()
    //{
    //    return db.Authors.ToList();
    //}

    //public Author GetAuthor(int id)
    //{
    //    return db.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == id);
    //}

    //public string CreateBook(string title, int authorId)
    //{
    //    Author author = db.Authors.FirstOrDefault(a => a.AuthorId == authorId);
    //    db.Books.Add(new Book { Title = title, Author = author });
    //    db.SaveChanges();
    //    return "Book created";
    //}

}