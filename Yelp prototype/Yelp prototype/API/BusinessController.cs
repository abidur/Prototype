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

        [HttpGet]
        public List<Business> Get(string categoryList, string Location, string radius, bool isKidFriendly)
        {
            Request.CreateResponse(HttpStatusCode.OK);
            return BusinessHelper.GetBusinesses(categoryList, Location, radius, isKidFriendly);
        }        
    }
}