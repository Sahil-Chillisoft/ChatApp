#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string? validationException)
        {
            if (!string.IsNullOrEmpty(validationException))
                ViewBag.ValidationMessage = validationException;

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username)
        {
            if (string.IsNullOrEmpty(username))
                return RedirectToAction($"Index", new { validationException = "Please enter a username." });

            if (!SqlHelper.IsExistingUser(username))
                SqlHelper.AddNewUser();

            return RedirectToAction("Index", $"Dashboard");
        }
    }
}
