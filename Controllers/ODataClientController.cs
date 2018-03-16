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


namespace ODataClient.Controllers
{
    [Route("api/[controller]")]
    public class ODataClientController : Controller
    {
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public IActionResult Error()
        // {
        //     ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        //     return View();
        // }

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
            HttpClient httpClient = new HttpClient();
            var res = await httpClient.GetStringAsync(url);
            return new JsonResult(res);
        }
    }
}
