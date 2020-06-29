namespace NiceOne.Data.Entities
{
    using System;

    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
