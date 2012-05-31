using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpSharp.Data;

namespace Yelp_prototype.Models
{
    [Serializable]
    public class YelpResult
    {
        public int Hour { get; set; }
        public YelpResult(int _hour, Business _business){
            Hour = _hour;
            Business = _business;
        }
        public Business Business { get; set; }
    }
}