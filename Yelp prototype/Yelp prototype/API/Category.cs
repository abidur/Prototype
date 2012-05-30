using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Web.Script.Serialization;

namespace Yelp_prototype.API.Controllers
{
    public class CategoryNameController : ApiController
    {
        
        [HttpGet]
        public IEnumerable<String> Get(string input = "")
        {
            var xFileLocation = HostingEnvironment.MapPath("~/App_LocalResources/YelpCategories.xml");
            XDocument loaded = XDocument.Load(xFileLocation);
            var q = from c in loaded.Descendants("category")                    
                    select (string)c.Element("FriendlyName");            
            return q.AsEnumerable<string>();
        }
        
    }
}