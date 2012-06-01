using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpSharp.Data;
using YelpSharp;
using System.Configuration;
using YelpSharp.Data.Options;
using Yelp_prototype.Models;

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

        private static List<int> times = new List<int>() { 8, 10, 12, 18, 20, 22 };

        private static Dictionary<int, string> setEvents = new Dictionary<int, string>()
        {
            {8 , "breakfast_brunch"},            
            {12 , "restaurants"},
            {18 , "restaurants"}
        };

        private static string setUpDefaultCategories(string categoryList) {
            foreach (string _event in setEvents.Values)
            {
                if (string.IsNullOrEmpty(categoryList))
                    categoryList = _event.ToLower();
                else
                    categoryList += "," + _event.ToLower();
            }
            return categoryList;
        }

        private static string populateRemainingCategories(string categoryList)
        {
            var currectCategories = categoryList.Split(',');
            var randomCategories = CategoryHelper.GetRandomCategories(currectCategories , times.Count() - currectCategories.Length);
            return categoryList + "," + string.Join("," , randomCategories);
        }
        
        private static List<YelpResult> insertDestinationsIntoTimeSlots(List<YelpResult> searchResults)
        {            
            List<YelpResult> retVal = new List<YelpResult>();
            //put the ones we know the time for into correct slot
            foreach (var _time in setEvents.Keys)
            {                
                var destination = searchResults[0];
                searchResults.Remove(searchResults[0]);
                YelpResult yResult = new YelpResult(_time, destination.Business, destination.MatchingBusinesses);
                retVal.Add(yResult);
                searchResults.Remove(destination);
            }
            var openTimes = times.Where(f => !setEvents.Keys.Contains(f));
            foreach (var time in openTimes){ 
                var searchResult = searchResults.Take(1).FirstOrDefault();
                if (searchResult == null)
                    break;                
                searchResults.Remove(searchResult);
                YelpResult randomResult = new YelpResult(time, searchResult.Business, searchResult.MatchingBusinesses);
                retVal.Add(randomResult);
            }
            return retVal;
        }

        private static List<YelpResult> GenerateSchedule(string Location, string radius, bool isKidFriendly)
        {
            var o = GetOptions();
            var y = new Yelp(o);
            string categoryList = "";            
            categoryList = setUpDefaultCategories(categoryList);
            categoryList = populateRemainingCategories(categoryList);            
            var searchOptions = new YelpSharp.Data.Options.SearchOptions();
            var categories = categoryList.Split(',');
            List<YelpResult> results = new List<YelpResult>();
            foreach (var category in categories){
                searchOptions.GeneralOptions = new GeneralOptions()
                {
                    term = category
                };
                searchOptions.LocationOptions = new LocationOptions()
                {
                    location = Location,
                };
                var serchResult = y.Search(searchOptions).businesses;
                //TODO: IF no search result gotta figure out what else to do...
                if (serchResult == null || serchResult.Count() == 0)
                    continue;
                Random rnd = new Random();
                int r = rnd.Next(0 , serchResult.Count - 1);
                serchResult.Remove(serchResult[r]);
                YelpResult yResult = new YelpResult(0, serchResult[r], serchResult);
                results.Add(yResult);
            }            
            var retVals = insertDestinationsIntoTimeSlots(results);
            return retVals.OrderBy(f => f.Hour).ToList();
        }

        private static List<YelpResult> GetSingleDestinationOptions(string categoryList, string Location, string radius, bool isKidFriendly)
        {
            var o = GetOptions();
            var y = new Yelp(o);
            var searchOptions = new YelpSharp.Data.Options.SearchOptions();
            searchOptions.GeneralOptions = new GeneralOptions(){
                term = categoryList
            };
            searchOptions.LocationOptions = new LocationOptions(){
                location = Location,
            };
            var serchResults = y.Search(searchOptions).businesses;            
            var retVal = new YelpResult(0, serchResults[0], serchResults);
            return new List<YelpResult> { retVal } ;
        }

        public static List<YelpResult> GetBusinesses(string categoryList, string Location, string radius, bool isKidFriendly, bool generateSchedule)
        {            
            var radiusVal = Convert.ToInt32(radius);                        
            if (generateSchedule){
                return GenerateSchedule(Location, radius, isKidFriendly);
            }
            else{
                return GetSingleDestinationOptions(categoryList, Location, radius, isKidFriendly);
            }            
        }
        
    }
}