using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YelpSharp.Data;

namespace YelpTest.Models
{
    public class Agenda
    {
        public Business Business { get; set; }

        public int TimeSlot { get; set; }

        public string TimeString { get; set; }

        public static readonly Dictionary<int, string> TimeLine = new Dictionary<int, string>{
            {8,"Breakfast"},
            {10,"Activites for 10am"},
            {12,"Lunch"},
            {15,"Activities for 3pm"},
            {18,"Dinner"},
            {22,"Nightlife"},
        };

        public List<YelpSharp.Data.Business> AllOtherOptionsIntheSameCategory { get; set; }
    }
}