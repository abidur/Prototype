using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using YelpSharp;
using YelpSharp.Data;
using YelpSharp.Data.Options;
using YelpSharp.Util;
using YelpTest.Models;

namespace YelpTest.Controllers
{
    public class MyDayController : ApiController
    {
        // GET /api/default1
        public IList<Agenda> GetAll(double lat, double lon)
        {
            //var results = yelp.Search("coffee", "kansas city");

            var categoriesXML = XDocument.Load(HttpContext.Current.Server.MapPath(@"\categories.xml"));
            //var BreakFastCategories = categoriesXML.Descendants("category").Where(a => (string)a.Attribute("yelpName") == "breakfast_brunch");

            var userAgenda = new UserAgenda();
            userAgenda.Agendas = new List<Agenda>();
            var getBreakfastOptions = GetSearchResults(lat, lon, "breakfast_brunch");

            var randomIndex = new Random().Next(20);
            userAgenda.Agendas.Add(new Agenda { Business = getBreakfastOptions.businesses[randomIndex], TimeSlot = 8, TimeString = Agenda.TimeLine[8], AllOtherOptionsIntheSameCategory = getBreakfastOptions.businesses.Where((b, i) => i != randomIndex).ToList() });
            var tenAmCategories = string.Join(",", categoriesXML.Descendants("category").Where(a => (int)a.Element("time") == 10).Select(a => a.Attribute("yelpName")).Select(b => b.Value).ToArray());
            var tenAmOptions = GetSearchResults(lat, lon, tenAmCategories);
            userAgenda.Agendas.Add(new Agenda { Business = tenAmOptions.businesses[randomIndex], TimeSlot = 10, TimeString = Agenda.TimeLine[10], AllOtherOptionsIntheSameCategory = tenAmOptions.businesses.Where((b, i) => i != randomIndex).ToList() });
            var lunchOptions = GetSearchResults(lat, lon, "restaurants");
            userAgenda.Agendas.Add(new Agenda { Business = getBreakfastOptions.businesses[randomIndex], TimeSlot = 18, TimeString = Agenda.TimeLine[12], AllOtherOptionsIntheSameCategory = lunchOptions.businesses.Where((b, i) => i != randomIndex).ToList() });
            var fifteenCategories = string.Join(",", categoriesXML.Descendants("category").Where(a => (int)a.Element("time") == 15).Select(a => a.Attribute("yelpName")).Select(b => b.Value).ToArray());
            var fifteenOptions = GetSearchResults(lat, lon, tenAmCategories);
            userAgenda.Agendas.Add(new Agenda { Business = fifteenOptions.businesses[randomIndex], TimeSlot = 15, TimeString = Agenda.TimeLine[15], AllOtherOptionsIntheSameCategory = fifteenOptions.businesses.Where((b, i) => i != randomIndex).ToList() });
            var dinnerOptions = GetSearchResults(lat, lon, "restaurants");
            userAgenda.Agendas.Add(new Agenda { Business = dinnerOptions.businesses[randomIndex], TimeSlot = 18, TimeString = Agenda.TimeLine[18], AllOtherOptionsIntheSameCategory = dinnerOptions.businesses.Where((b, i) => i != randomIndex).ToList() });
            var nightCategories = string.Join(",", categoriesXML.Descendants("category").Where(a => (int)a.Element("time") == 22).Select(a => a.Attribute("yelpName")).Select(b => b.Value).ToArray());
            var nightOptions = GetSearchResults(lat, lon, tenAmCategories);
            userAgenda.Agendas.Add(new Agenda { Business = nightOptions.businesses[randomIndex], TimeSlot = 22, TimeString = Agenda.TimeLine[22], AllOtherOptionsIntheSameCategory = nightOptions.businesses.Where((b, i) => i != randomIndex).ToList() });
            return userAgenda.Agendas;
        }

        private static SearchResults GetSearchResults(double lat, double lon, string categories)
        {
            Yelp yelp = new Yelp(WebApiApplication.YelpOptions);
            var results = yelp.Search(new SearchOptions
            {
                GeneralOptions = new GeneralOptions { category_filter = categories, radius_filter = 30000, term = "", limit = 20, offset = 0, sort = 2, deals_filter = false },
                LocationOptions = new CoordinateOptions
                {
                    latitude = lat,
                    longitude = lon
                }
            });
            return results;
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