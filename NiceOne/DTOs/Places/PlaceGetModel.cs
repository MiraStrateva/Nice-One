using System.Collections.Generic;

namespace NiceOne.DTOs.Places
{
    public class PlaceGetModel
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int CategoryId { get; }
        public string CategoryName { get; }
        public int CityId { get; }
        public string City { get; }
        public string CountryId { get; }
        public string Country { get; }
        public decimal Rating { get; }
        public int FeedbackCount { get; }
        public ICollection<PlaceFeedbackGetModel> Feedbacks { get; } = new List<PlaceFeedbackGetModel>();
        public ICollection<PlacePictureGetModel> Pictures { get; } = new List<PlacePictureGetModel>();
    }
}
