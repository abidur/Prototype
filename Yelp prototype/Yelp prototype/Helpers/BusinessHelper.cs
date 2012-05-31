﻿using System;
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

        private static List<int> times = new List<int>() { 8, 10, 12, 14, 16, 18,  20, 22 };

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
            return categoryList + "," + string.Join("," , randomCategories);
        }

        private static List<string> GetCategoriesFromBusiness(Business b)
        {             
            return b.categories.SelectMany(x => x).ToList();
        }

        private static List<YelpResult> insertDestinationsIntoTimeSlots(List<Business> searchResults)
        {            
            List<YelpResult> retVal = new List<YelpResult>();
            //put the ones we know the time for into correct slot
            foreach (var _time in setEvents.Keys)
            {
                var yelpName = CategoryHelper.GetYelpCategoryName(setEvents[_time].ToLower());
                //business.categories[1] is the index at which the API Queryable name lives                
                var destination = searchResults.Where(f => GetCategoriesFromBusiness(f).Contains(yelpName)).FirstOrDefault();                
                searchResults.Remove(destination);
                YelpResult yResult = new YelpResult(_time, destination);
                retVal.Add(yResult);
            }
            var openTimes = times.Where(f => !setEvents.Keys.Contains(f));
            foreach (var time in openTimes)
            { 
                var searchResult = searchResults.Take(1).FirstOrDefault();
                if (searchResult == null)
                    break;                
                searchResults.Remove(searchResult);
                YelpResult randomResult = new YelpResult(time, searchResult);
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
            List<Business> results = new List<Business>();
            foreach (var category in categories)
            {
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
                results.Add(serchResult[r]);
            }            
            var retVals = insertDestinationsIntoTimeSlots(results);
            return retVals;
        }



        private static List<YelpResult> GetSingleDestination(string categoryList, string Location, string radius, bool isKidFriendly)
        {
            //TODO:Implement
            var o = GetOptions();
            var y = new Yelp(o);
            return null;
        }

        public static List<YelpResult> GetBusinesses(string categoryList, string Location, string radius, bool isKidFriendly, bool generateSchedule)
        {            
            var radiusVal = Convert.ToInt32(radius);                        
            if (generateSchedule){
                return GenerateSchedule(Location, radius, isKidFriendly);
            }
            else{
                return GetSingleDestination(categoryList, Location, radius, isKidFriendly);
            }            
        }
        
    }
}