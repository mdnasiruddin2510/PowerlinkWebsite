﻿using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        }
    }
}