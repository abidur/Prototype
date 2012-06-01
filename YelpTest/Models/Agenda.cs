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

        public static readonly Dictionary<int, string> TimeLine = new Dictionary<int, string>{
            {1,"8am to 9am"},
            {2,"9am to 10am"},
            {3,"10am to 11am"},
            {4,"12pm to 1pm"},
            {5,"1pm to 2pm"},
            {6,"2pm to 3pm"},
            {7,"3pm to 4pm"},
            {8,"4pm to 5pm"},
            {9,"5pm to 6pm"},
            {10,"7pm to 8pm"},
            {11,"8pm to 9pm"},
            {12,"10pm to 11pm"},
            {13,"11pm to 12am"}
        };
    }
}