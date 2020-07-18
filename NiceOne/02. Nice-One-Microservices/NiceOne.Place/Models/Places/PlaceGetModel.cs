namespace NiceOne.Place.Models.Places
{
    using System.Collections.Generic;

    public class PlaceGetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public double Rating { get; set; }
        public int FeedbackCount { get; set; }
        public IEnumerable<PlaceFeedbackGetModel> Feedbacks { get; set; } = new List<PlaceFeedbackGetModel>();
    }
}
