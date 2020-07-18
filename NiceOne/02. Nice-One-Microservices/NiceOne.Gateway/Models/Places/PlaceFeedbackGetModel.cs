namespace NiceOne.Gateway.Models.Places
{
    using System;

    public class PlaceFeedbackGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string User { get; set; }
    }
}
