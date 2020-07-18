namespace NiceOne.Client.Models.Place.Categories
{
    public class CategoryGetModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public int PlacesCount { get; set; }
    }
}
