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
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> stylistClients = selectedStylist.GetAllClients();
        List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
        List<Specialty> allSpecialties = Specialty.GetAll();
        model.Add("selectedStylist", selectedStylist);
        model.Add("stylistClients", stylistClients);
        model.Add("stylistSpecialties", stylistSpecialties);
        model.Add("allSpecialties", allSpecialties);
        return View(model);
      }

      [HttpGet("/stylists/edit/{id}")]
      public ActionResult EditStylistForm(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        return View("EditStylist", thisStylist);
      }

      [HttpPost("/stylists/edit/{id}")]
      public ActionResult EditStylist(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        thisStylist.Edit(Request.Form["edit-stylist-name"], Int32.Parse(Request.Form["edit-stylist-phone"]), Request.Form["edit-stylist-email"], Int32.Parse(Request.Form["edit-stylist-experience"]));
        return RedirectToAction("AllStylists");
      }

      [HttpPost("/stylists/{stylistId}/specialties/new")]
      public ActionResult AddSpecialtyToStylist(int stylistId)
      {
        Stylist stylist = Stylist.Find(stylistId);
        Specialty specialty = Specialty.Find(Int32.Parse(Request.Form["specialty-id"]));
        stylist.AddSpecialty(specialty);
        return RedirectToAction("StylistDetails", new {id = stylistId});
      }

      [HttpPost("/stylists/delete/{id}")]
      public ActionResult DeleteStylist(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        thisStylist.Delete();
        return RedirectToAction("AllStylists");
      }

      [HttpPost("/stylists/delete/all")]
      public ActionResult DeleteAllStylists()
      {
        Stylist.DeleteAll();
        return RedirectToAction("AllStylists");
      }
}
}
