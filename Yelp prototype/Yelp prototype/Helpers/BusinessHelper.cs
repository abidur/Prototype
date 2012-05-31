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

        private static List<int> times = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };

        private static Dictionary<int, string> setEvents = new Dictionary<int, string>()
        {
            {8 , "Breakfast"},            
            {12 , "Food"},
            {19 , "Food"}
        };

        private static string setUpDefaultCategories(string categoryList) {
            foreach (string _event in setEvents.Values)
            {
                if (string.IsNullOrEmpty(categoryList))
                    categoryList = CategoryHelper.GetYelpCategoryName(_event.ToLower());
                else
                    categoryList += "," + CategoryHelper.GetYelpCategoryName(_event.ToLower());
            }
            return categoryList;
        }


        private static string populateRemainingCategories(string categoryList)
        {
            var currectCategories = categoryList.Split(',');
            var randomCategories = CategoryHelper.GetRandomCategories(currectCategories , times.Count() - currectCategories.Length);
            return categoryList + string.Join("," , randomCategories);
        }

        private static List<Business> insertDestinationsIntoTimeSlots(List<Business> searchResults)
        {
            //TODO: IMPLEMENT
            return null;
        }


        public static List<Business> GetBusinesses(string categoryList, string Location, string radius, bool isKidFriendly)
        {
            var o = GetOptions();
            var y = new Yelp(o);
            var radiusVal = Convert.ToInt32(radius);            
            //We aren't searching for a specific destination replacement;
            if (string.IsNullOrEmpty(categoryList)){
                categoryList = setUpDefaultCategories(categoryList);
                categoryList = populateRemainingCategories(categoryList);
            }
            var searchOptions = new YelpSharp.Data.Options.SearchOptions();
            searchOptions.GeneralOptions = new GeneralOptions(){
                term = categoryList
            };
            searchOptions.LocationOptions = new LocationOptions(){
                location = Location,
            };
            var results = y.Search(searchOptions).businesses;
            var retVals = insertDestinationsIntoTimeSlots(results);
            return retVals;
        }
    }
}