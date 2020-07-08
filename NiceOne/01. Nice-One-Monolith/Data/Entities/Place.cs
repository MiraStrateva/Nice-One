namespace NiceOne.Data.Entities
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
        public City City { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
