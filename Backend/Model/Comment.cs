using System;
namespace Model
{
    public class Comment
    {
        public Comment()
        {

        }

        public Comment(string description, string user)
        {
            this.Description = description;
            this.User = user;
            this.Date = DateTime.Now;
            this.Votes = 0;
        }

        public long CommentID { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public int Votes { get; set; }
        public Topic Topic { get; set; }
    }
}


