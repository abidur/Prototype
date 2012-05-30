using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Yelp_prototype.Helpers
{
    public static class CategoryHelper
    {
        public static IEnumerable<string> GetYelpCategoryNames(string friendlyCategoryNames)
        {
            var FriendlyCategoryNames = friendlyCategoryNames.Split(',');            
            var xFileLocation = HostingEnvironment.MapPath("~/App_LocalResources/YelpCategories.xml");
            XDocument loaded = XDocument.Load(xFileLocation);
            var q = from c in loaded.Descendants("category")
                    where friendlyCategoryNames.Contains(c.Element("FriendlyName").Value.ToString().ToLower())
                    select (string)c.Element("yelpName");
            return q.AsEnumerable<string>();
        }
    }
}