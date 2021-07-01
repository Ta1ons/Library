namespace Library.Models
{
    public class Book
    {
        public int ID;
        public string title;
        public string author;
        public string series;
        public int overallRating; //average of review star rating. To be moved into services at a later time.
    }
}
