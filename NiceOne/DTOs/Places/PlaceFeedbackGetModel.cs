using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceOne.DTOs.Places
{
    public class PlaceFeedbackGetModel
    {
        public string Text { get; }
        public int Rating { get; }
        public DateTime Date { get; }
        public string User { get; }
    }
}
