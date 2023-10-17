using Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service
{

    internal class DataService
    {
        private TopicContext db { get; }

        public DataService(TopicContext db)
        {
            this.db = db;
        }

        // Boards
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
        public List<Comment> GetComments(int topicId)
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
}
