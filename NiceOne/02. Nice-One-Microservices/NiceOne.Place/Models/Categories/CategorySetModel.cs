namespace NiceOne.Place.Models.Categories
{
    using Microsoft.AspNetCore.Http;

    public class CategorySetModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;
        public IFormFile FormFile { get; set; }
    }
}
