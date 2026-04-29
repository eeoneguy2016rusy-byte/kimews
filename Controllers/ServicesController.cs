using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;

namespace PhotoStudio.Controllers;

public class ServicesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServicesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. СПИСОК УСЛУГ
    public async Task<IActionResult> Index()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrEmpty(userRole))
            return RedirectToAction("Login", "Account");
        return View(await _context.Services.ToListAsync());
    }

    public IActionResult Create()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Service service)
    {
        if (ModelState.IsValid)
        {
            _context.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(service);
    }

    // 4. УДАЛЕНИЕ
    public async Task<IActionResult> Delete(int? id)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}