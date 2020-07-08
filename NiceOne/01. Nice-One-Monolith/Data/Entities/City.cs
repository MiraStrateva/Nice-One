namespace NiceOne.Data.Entities
{
    using System.Collections.Generic;

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
