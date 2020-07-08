namespace NiceOne.DTOs.Feedbacks
{
    using System;
    public class FeedbackGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int PlaceId { get; set; }
        public string Place { get; set; }
        public string UserId { get; set; }
        public string User { get; set; }
    }
}
