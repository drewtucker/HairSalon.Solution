using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalonApp.Models;

namespace HairSalonApp.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/stylist/delete_all")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();

            return RedirectToAction("Index");
        }
    }
}
