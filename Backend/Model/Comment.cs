using System;
namespace Model
{
    public class Comment
    {
        public Comment()
        {

        }

        public Comment(string description, string user, DateTime date, int votes)
        {
            this.Description = description;
            this.User = user;
            this.Date = date;
            this.Votes = votes;
        }

        public long CommentID { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public int Votes { get; set; }
        public Topic topic { get; set; }
    }
}

