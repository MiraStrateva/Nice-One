namespace NiceOne.Place.Data.Entities
{
    using System.Collections.Generic;

    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
