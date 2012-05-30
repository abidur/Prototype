using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpSharp.Data;
using YelpSharp;
using System.Configuration;
using YelpSharp.Data.Options;

namespace Yelp_prototype.Helpers
{
    public static class BusinessHelper
    {
        #region GetOptions
        /// <summary>
        /// return the oauth options in this case from app.config
        /// </summary>
        /// <returns></returns>
        private static Options GetOptions()
        {
            return new Options()
            {
                AccessToken = ConfigurationManager.AppSettings["AccessToken"],
                AccessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"],
                ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"]
            };
        }
        #endregion

        public static List<Business> GetBusinesses(string categoryList = "coffee", string Location = "Kansas City, mo", string radius = "25")
        {
            var o = GetOptions();
            var y = new Yelp(o);
            var radiusVal = Convert.ToInt32(radius);
            categoryList = string.Join(",", CategoryHelper.GetYelpCategoryNames(categoryList.ToLower()));
            var searchOptions = new YelpSharp.Data.Options.SearchOptions();
            searchOptions.GeneralOptions = new GeneralOptions()
            {
                term = categoryList
            };
            searchOptions.LocationOptions = new LocationOptions()
            {
                location = Location,
            };
            var results = y.Search(searchOptions);            
            return results.businesses;
        }
    }
}