using System;
namespace Model
{
    public class Topic
    {
        public Topic()
        {

        }

        public Topic(string title, string description, string user)
        {
            this.Title = title;
            this.Description = description;
            this.User = user;
            this.Date = DateTime.Now;
            this.Votes = 0;
        }

      

        public long TopicID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public int Votes = 0;

        public List<Comment> Comment { get; set; }

    }
}

