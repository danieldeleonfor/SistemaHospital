using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaHospitalario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(GeneralConfig.userSessionKey) == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Usuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            return View();
        }

        public IActionResult FakeLogin()
        {
            HttpContext.Session.SetString(GeneralConfig.userSessionKey, "Jorge");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            HttpContext.Session.SetString(GeneralConfig.userSessionKey, model.Username);
            
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
