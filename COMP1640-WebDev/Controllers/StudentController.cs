﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640_WebDev.Controllers
{

/*    [Authorize(Roles = "Student")]
*/    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
