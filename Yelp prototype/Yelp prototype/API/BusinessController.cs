using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Yelp_prototype.Models;
using System.Net;
using YelpSharp;
using System.Configuration;
using YelpSharp.Data;
using System.Xml.Linq;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using YelpSharp.Data.Options;
using Yelp_prototype.Helpers;

namespace Yelp_prototype.API.Controllers
{
    public class BusinessController : ApiController
    {
        // GET /api/<controller>
        #region GetOptions
        /// <summary>
        /// return the oauth options in this case from app.config
        /// </summary>
        /// <returns></returns>
        protected Options GetOptions()
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

        [HttpGet]
        public List<Business> Get(string categoryList = "coffee", string Location="Kansas City, mo", string radius ="25")
        {
            var o = GetOptions();
            var y = new Yelp(o);                                  
            var radiusVal = Convert.ToInt32(radius);
            categoryList = string.Join(",",CategoryHelper.GetYelpCategoryNames(categoryList.ToLower()));
            var searchOptions = new YelpSharp.Data.Options.SearchOptions();
            searchOptions.GeneralOptions = new GeneralOptions(){
                term = categoryList
            };
            searchOptions.LocationOptions = new LocationOptions(){
                location = Location,                
            };
            var results = y.Search(searchOptions);
            Request.CreateResponse(HttpStatusCode.OK);
            return results.businesses;
        }

        //[HttpPost]
        //public List<Business> GenerateSchedule(string searchText, DateTime startTime, DateTime endTime)
        //{
        //    var validDestinations = Get(searchText);
        //    return null;
        //}

        

    }
}