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
        public static string GetYelpCategoryName(string friendlyCategoryName)
        {            
            var xFileLocation = HostingEnvironment.MapPath("~/App_LocalResources/YelpCategories.xml");
            XDocument loaded = XDocument.Load(xFileLocation);
            var q = from c in loaded.Descendants("category")
                    where c.Element("FriendlyName").Value.ToString().ToLower() == friendlyCategoryName
                    select (string)c.Element("yelpName");
            return q.FirstOrDefault();
        }

        public static List<string> GetRandomCategories(string[] excludedCategories, int resultCount)
        {
            var xFileLocation = HostingEnvironment.MapPath("~/App_LocalResources/YelpCategories.xml");
            XDocument loaded = XDocument.Load(xFileLocation);
            var q = from c in loaded.Descendants("category")
                    where !excludedCategories.Contains(c.Element("FriendlyName").Value.ToString().ToLower())
                    select (string)c.Element("yelpName");
            //TODO:Will this actually be random? Probalby not. WIll need to refactor
            return q.ToList().Take(resultCount).ToList();
        }
    }
}