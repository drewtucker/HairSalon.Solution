using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalonApp.Models;

namespace HairSalonApp.Controllers
{
    public class StylistsController : Controller
    {
      [HttpGet("/stylists")]
      public ActionResult AllStylists()
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
      }

      [HttpGet("/stylists/new")]
      public ActionResult NewStylist()
      {
        return View();
      }

      [HttpPost("/stylists")]
      public ActionResult AddStylist()
      {
        string name = Request.Form["stylist-name"];
        int number = Int32.Parse(Request.Form["stylist-phone"]);
        string email = Request.Form["stylist-email"];
        int experience = Int32.Parse(Request.Form["stylist-experience"]);
        Stylist newStylist = new Stylist(name, number, email, experience);
        newStylist.Save();
        return View("AllStylists", newStylist);
      }

      [HttpGet("/stylists/details/{id}")]
        public ActionResult StylistDetails(int id)
        {
            return View(Stylist.Find(id));
        }


      [HttpPost("/stylists/delete/{id}")]
      public ActionResult DeleteStylist(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        thisStylist.Delete();
        return RedirectToAction("AllStylists");
      }
}
}
