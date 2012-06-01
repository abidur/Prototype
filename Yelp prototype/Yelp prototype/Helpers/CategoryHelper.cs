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
            var AllCategorieCount = 
                (from c in loaded.Descendants("category")
                 select (string)c.Element("yelpName")).Count(); ;
            var AllEligableCategories = from c in loaded.Descendants("category")
                    where !excludedCategories.Contains(c.Element("FriendlyName").Value.ToString().ToLower())
                    select (string)c.Element("yelpName");            
            List<string> retVals = new List<string>();
            for (int i = 0; i < resultCount; i++ )
            {
                Random rnd = new Random();
                int randomIndex = rnd.Next(0, AllEligableCategories.Count() - 1);
                retVals.Add(AllEligableCategories.ToList().Skip(randomIndex - 1).Take(1).Single());
            }
            return retVals;
        }
    }
}