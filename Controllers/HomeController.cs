using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Models;

namespace PhotoStudio.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrEmpty(userRole))
            return RedirectToAction("Login", "Account");
        return View();
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
