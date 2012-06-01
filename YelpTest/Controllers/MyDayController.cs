using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using YelpSharp;
using YelpSharp.Data;
using YelpSharp.Data.Options;
using YelpSharp.Util;

namespace YelpTest.Controllers
{
    public class MyDayController : ApiController
    {
        // GET /api/default1
        public List<Business> GetAll(double lat, double lon)
        {
            Yelp yelp = new Yelp(WebApiApplication.YelpOptions);
            //var results = yelp.Search("coffee", "kansas city");

            var results = yelp.Search(new SearchOptions
            {
                GeneralOptions = new GeneralOptions { category_filter = "food", radius_filter = 30000, term = "coffee" },
                LocationOptions = new CoordinateOptions
                {
                    latitude = lat,
                    longitude = lon
                }
            });

            return results.businesses;
        }

        // GET /api/default1/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST /api/default1
        public void Post(string value)
        {
        }

        // PUT /api/default1/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/default1/5
        public void Delete(int id)
        {
        }
    }
}