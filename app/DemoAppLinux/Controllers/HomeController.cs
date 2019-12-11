﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoAppLinux.Models;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemoAppLinux.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(Attendee attendeeInput)
        {
            if (!string.IsNullOrEmpty(attendeeInput?.Name))
            {
                using (var writer = new StreamWriter(@"./Attendee.json"))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(new Attendee { Name = attendeeInput.Name }));
                }
            }
            Attendee attendee = null;
            using (StreamReader file = System.IO.File.OpenText(@"./Attendee.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject attendeeJson = (JObject)JToken.ReadFrom(reader);
                attendee = attendeeJson.ToObject<Attendee>();
            }
            return View(attendee);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
