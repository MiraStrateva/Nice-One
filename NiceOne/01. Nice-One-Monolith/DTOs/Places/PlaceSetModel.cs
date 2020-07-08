namespace NiceOne.DTOs.Places
{
    using NiceOne.Data.Entities;

    using System.Collections.Generic;

    public class PlaceSetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; }
    }
}
