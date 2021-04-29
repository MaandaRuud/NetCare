using DocWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(String Search)
        {
            try
            {
                using (var context = new Entities())
                {
                    List<Models.EF.Doctor> Doctors = new List<Doctor>();
                    if (Search == null)
                    {
                        Doctors = context.Doctors.OrderBy(o => o.Lastname).ToList();
                    }
                    else
                    {
                        Doctors = context.Doctors.Where(x => x.Firstname.Contains(Search) || x.Lastname.Contains(Search) || x.HCPSANo.Contains(Search)).OrderBy(o => o.Lastname).ToList();
                    }
                    var titles = context.Titles.Where(x => x.IsActive == true).ToList();
                    var disciplines = context.Disciplines.Where(x => x.IsActive == true).ToList();
                    var provinces = context.Provinces.Where(x => x.IsActive == true).ToList();
                    Doctors.ForEach(doc =>
                    {
                        doc.Title = titles.Where(x => x.TitleId == doc.TitleId).FirstOrDefault();
                        doc.Discipline = disciplines.Where(x => x.DisciplineId == doc.DisciplineId).FirstOrDefault();
                        doc.Province = provinces.Where(x => x.ProvinceId == doc.ProvinceId).FirstOrDefault();
                    });
                    ViewBag.Doctors = Doctors;
                }
                return View();
            }
            catch (Exception error)
            {
                return View("Error", error);
            }
        }

        public ActionResult Doctor(int id)
        {
            try
            {
                Models.EF.Doctor doctor = new Doctor();
                using (var context = new Entities())
                {
                    doctor = context.Doctors.Where(x => x.DoctorId == id).FirstOrDefault();
                    doctor.Title = context.Titles.Where(x => x.TitleId == doctor.TitleId).FirstOrDefault();
                    doctor.Discipline = context.Disciplines.Where(x => x.DisciplineId == doctor.DisciplineId).FirstOrDefault();
                    doctor.Province = context.Provinces.Where(x => x.ProvinceId == doctor.ProvinceId).FirstOrDefault();
                    doctor.Region = context.Regions.Where(x => x.RegionId == doctor.RegionId).FirstOrDefault();
                }
                return View(doctor);
            }
            catch (Exception error)
            {
                return View("Error", error);
            }
        }

        public ActionResult Admin()
        {
            try
            {
                var user = HttpContext.User;
                using (var context = new Entities())
                {
                    var aspuser = context.AspNetUsers.Where(x => x.UserName == user.Identity.Name).FirstOrDefault();
                    if (aspuser.AspNetRoles.Where(x => x.Name == "Admin").FirstOrDefault() == null)
                    {
                        return View("Restricted");
                    }
                    ViewBag.Disciplines = context.Disciplines.Where(x => x.IsActive == true).ToList();
                    ViewBag.Provinces = context.Provinces.Where(x => x.IsActive == true).ToList();
                    ViewBag.Regions = context.Regions.Where(x => x.IsActive == true).ToList();
                    ViewBag.Titles = context.Titles.Where(x => x.IsActive == true).ToList();
                }
                return View();
            }
            catch (Exception error)
            {
                return View("Error",error);
            }
        }

        [HttpPost]
        public ActionResult Admin(Models.EF.Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Admin");
            }
            try
            {
                using (var context = new Entities())
                {
                    context.Doctors.Add(doctor);
                    context.SaveChanges();
                }
                return View("Success");
            }
            catch (Exception error)
            {
                return View("Error", error);
            }
        }

        public ActionResult Success()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Restricted()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Error(Exception error)
        {
            ViewBag.Error = error;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}