using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InterviewTask.API;
using Microsoft.AspNetCore.Mvc;
using InterviewTask.Models;
using Microsoft.Extensions.Configuration;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
	    private readonly APIManager _apiManager;
        public HomeController(IConfiguration configuration)
        {
	        _apiManager = APIManager.Current();
        }

	    public async Task<IActionResult> Index()
	    {
		    var geo =  await _apiManager.GetGeoPosition("Lviv");
            return View();
        }


    }
}
