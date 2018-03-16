using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ODataClient.Model;

namespace ODataClient.Controllers
{
    [Route("api/[controller]")]
    public class ODataClientController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        [Route("GetPeople")]
         public  async Task<string> GetPeople()
        {
             var stringTask = client.GetStringAsync("http://services.odata.org/v4/TripPinServiceRW/People");

             var msg= await stringTask;

             return msg;
              
        }

        [HttpGet]
        public async Task<IActionResult> QueryData(string results, string filter, string filterValue)
        {
            if (string.IsNullOrEmpty(results) ||
            string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(filterValue))
            {
                throw new ArgumentException("Parameter incorrect");
            }

            int resultCount;
            int filterVal;
            if(!int.TryParse(results, out resultCount))
            {
                throw new ArgumentOutOfRangeException("results incorrect");
            }
            if(!int.TryParse(filterValue, out filterVal))
            {
                throw new ArgumentOutOfRangeException("filterValue incorrect");
            }

            var criteria = string.Empty;
            
            if(filter=="1")
                criteria = "lt";
            if(filter=="2")
                criteria = "gt";

            var baseUrl = "http://services.odata.org/v4/(S(34wtn2c0hkuk5ekg0pjr513b))/TripPinServiceRW/";
            var url = baseUrl + "People?$top=" +resultCount + " & $select=FirstName, LastName & $filter=Trips/any(d:d/Budget "+ criteria +" "+filterValue +")";

            var res = await client.GetStringAsync(url);
            return new JsonResult(res);
        }
    }
}
