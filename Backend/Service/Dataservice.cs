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



    // Topics
    public List<Topic> GetTopics()
    {
        return db.Topics.Include(t => t.Comment).OrderByDescending(d => d.Date).ToList();
    }
    public Topic GetTopic(int id)
    {
        return db.Topics.Include(t => t.Comment).FirstOrDefault(b => b.TopicID == id)!;
    }

    public Topic CreateTopic(string title, string description, string user, DateTime date, int votes)
    {

        Topic topic = new Topic
        {
            Title = title,
            Description = description,
            User = user,
            Date = date,
            Votes = votes
        };

        db.Topics.Add(topic);
        db.SaveChanges();
        return topic;
    }

    // Comments

    public List<Comment> GetComments(int topicId)
    {
        return db.Topics.Include(t => t.Comment).FirstOrDefault(b => b.TopicID == topicId)!.Comment.ToList();
    }

    public Comment GetComment(int topicId)
    {
        return db.Comment.First(c => c.CommentID == topicId);
    }


    public Comment CreateComment(string description, string user, int topicid)
    //public Comment CreateComment(string description, string user)
    {

        //Comment newcomment = new Comment(description, user);

        Topic topic1 = db.Topics.FirstOrDefault(a => a.TopicID == topicid);

        Comment comment = new Comment
        {
            Description = description,
            User = user,
            Date = DateTime.Now,
            Votes = 0,
            Topic = topic1
        };

        db.Comment.Add(comment);

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
        return comment;

    }

    public Topic UpvoteTopic(int topicId)
    {
        var topic = db.Topics.SingleOrDefault(t => t.TopicID == topicId);

        topic.Votes++;
        db.SaveChanges(); // Save the changes to the database
        return topic; // Return the updated topic
    }

    public Topic DownvoteTopic(int topicId)
    {
        var topic = db.Topics.SingleOrDefault(t => t.TopicID == topicId);

        topic.Votes--;
        db.SaveChanges();
        return topic;
    }


    public Comment UpvoteComment(int commentid, int topicid)
    {
        var comment = db.Comment.SingleOrDefault(t => t.CommentID == commentid);
        var topic = db.Topics.SingleOrDefault(t => t.TopicID == topicid);


        comment.Votes++;
        db.SaveChanges(); // Save the changes to the database
        return comment; // Return the updated topic
    }

    public Comment DownvoteComment(int commentId, int topicid)
    {
        var comment = db.Comment.SingleOrDefault(t => t.CommentID == commentId);
        var topic = db.Topics.SingleOrDefault(t => t.TopicID == topicid);

        comment.Votes--;
        db.SaveChanges();
        return comment;
    }

}
