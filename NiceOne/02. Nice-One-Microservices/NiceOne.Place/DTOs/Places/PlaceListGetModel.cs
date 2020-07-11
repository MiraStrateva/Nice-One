namespace NiceOne.Place.DTOs.Places
{
    public class PlaceListGetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Rating { get; set; }
        public int FeedbackCount { get; set; }
    }
}
