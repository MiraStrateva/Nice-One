using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceOne.DTOs.Places
{
    public class PlaceListGetModel
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string CategoryName { get; }
        public string City { get; }
        public string Country { get; }
        public decimal Rating { get; }
        public int FeedbackCount { get; }
        public string ImageUrl { get; } 
    }
}
