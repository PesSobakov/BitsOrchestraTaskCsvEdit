using System.Diagnostics;
using BitsOrchestraTaskCsvEdit.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitsOrchestraTaskCsvEdit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("index");
        }
    }
}
