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
        
        public static List<string> GetRandomCategories(string[] excludedCategories, int resultCount)
        {            
            var xFileLocation = HostingEnvironment.MapPath("~/App_LocalResources/MakeMyDay.xml");
            XDocument loaded = XDocument.Load(xFileLocation);                       
            var AllEligableCategories = (from c in loaded.Descendants("category")                                        
                                        select (string)c.Element("yelpName")).Where(f => !excludedCategories.Contains(f));

            List<string> retVals = new List<string>();
            Random rnd = new Random();
            for (int i = 0; i < resultCount; i++ )
            {
                int randomIndex = rnd.Next(0, AllEligableCategories.Count() - 1);
                retVals.Add(AllEligableCategories.ToList().Skip(randomIndex - 1).Take(1).Single());
            }
            return retVals;
        }
    }
}