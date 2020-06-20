using Microsoft.Extensions.Diagnostics.HealthChecks;
using NiceOne.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceOne.DTOs.Places
{
    public class PlaceSetModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; }
        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
