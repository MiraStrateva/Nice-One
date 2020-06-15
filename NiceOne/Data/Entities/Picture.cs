namespace NiceOne.Data.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
