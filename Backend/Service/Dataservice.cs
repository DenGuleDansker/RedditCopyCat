using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    //public void SeedData()
    //{

    //    Topic topic = db.Topics.FirstOrDefault()!;
    //    if (topic == null)
    //    {
    //        topic = new Topic { Title = "Hej" };
    //        db.Topics.Add(topic);
  
    //    }

   
    //    db.SaveChanges();
    //}


    // Topics
    public List<Topic> GetTopics()
    {
        return db.Topics.Include(t => t.Comment).ToList();
    }
    public Topic GetTopic(int id)
    {
        return db.Topics.Include(t => t.Comment).FirstOrDefault(b => b.TopicID == id)!;
    }

    public string CreateTopic(Topic topicdata)
    {

        Topic topic = new Topic 
        {
        Title = topicdata.Title,
        Description = topicdata.Description,
        User = topicdata.User,
        Date = topicdata.Date,
        Votes = topicdata.Votes
        };

        db.Topics.Add(topic);
        db.SaveChanges();
        return "Topic created, id: " + topic.TopicID;
    }

    // Comments
    public List<Comment> GetComment(int topicId)
    {
        return db.Topics.Include(t => t.Comment).FirstOrDefault(b => b.TopicID == topicId)!.Comment.ToList();
    }

    public List<Comment> GetComments(int topicId, int commentId)
    {
        return db.Topics.Include(t => t.Comment).FirstOrDefault(b => b.TopicID == topicId)!.Comment.ToList();
    }


    public string CreateComment(string description, string user, int topicid)
    //public Comment CreateComment(string description, string user)
    {

        //Comment newcomment = new Comment(description, user);

        Topic topic = db.Topics.FirstOrDefault(a => a.TopicID == topicid);
        db.Comment.Add(new Comment { Description = description, User = user, Topic = topic });

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            // Fang fejlen og se på den indre undtagelse
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                Console.WriteLine(innerException.Message);
                innerException = innerException.InnerException;
            }
        }
        return "Comment created";

    }


}