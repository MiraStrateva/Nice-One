namespace NiceOne.Place.Models.Feedbacks
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    public class FeedbackSetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [HiddenInput]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int PlaceId { get; set; }
        public string UserId { get; set; }
    }
}
