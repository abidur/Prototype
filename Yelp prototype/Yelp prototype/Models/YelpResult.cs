using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpSharp.Data;
using System.Runtime.Serialization;

namespace Yelp_prototype.Models
{
    [DataContract]    
    public class YelpResult
    {
        [DataMember]
        public int Hour { get; set; }

        [DataMember]
        public Business Business { get; set; }

        public YelpResult(int _hour, Business _business){
            Hour = _hour;
            Business = _business;
        }

        
    }
}