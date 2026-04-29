using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;

namespace PhotoStudio.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await _context.Employees
            .FirstOrDefaultAsync(u => u.Email == model.Username);

        if (user != null)
        {
            HttpContext.Session.SetString("UserRole", user.Position);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FullName);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }

    public IActionResult Logout() => RedirectToAction("Login");
}