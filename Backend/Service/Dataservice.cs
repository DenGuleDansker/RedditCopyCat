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
    public void SeedData()
    {

        Topic topic = db.Topics.FirstOrDefault()!;
        if (topic == null)
        {
            topic = new Topic { Title = "Hej" };
            db.Topics.Add(topic);
  
        }

   
        db.SaveChanges();
    }


    // Topics
    public List<Topic> GetTopics()
    {
        return db.Topics.Include(t => t.Comment).ToList();
    }
    public Topic GetTopic(int id)
    {
        return db.Topics.Include(t => t.Comment).FirstOrDefault(b => b.TopicID == id)!;
    }

    public String CreateTopic()
    {
        Topic topic = new Topic();
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


    public string CreateComment(int topicID, string description, string user, DateTime date, int votes)
    {
        Topic topic = db.Topics.FirstOrDefault(b => b.TopicID == topicID);
        Comment comment = new Comment(description, user, date, votes);
        topic.Comment.Add(comment);
        db.SaveChanges();
        return "Comment created, id: " + comment.CommentID;
    }


}