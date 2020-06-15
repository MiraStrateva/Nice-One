using System.Collections.Generic;

namespace NiceOne.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
