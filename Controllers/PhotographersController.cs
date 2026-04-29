using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;

namespace PhotoStudio.Controllers;

public class PhotographersController : Controller
{
    private readonly ApplicationDbContext _context;
    public PhotographersController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrEmpty(userRole))
            return RedirectToAction("Login", "Account");
        return View(await _context.Photographers.ToListAsync());
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
    public async Task<IActionResult> Create(Photographer photographer)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        if (ModelState.IsValid)
        {
            _context.Add(photographer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(photographer);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        if (id == null)
            return NotFound();
        
        var photographer = await _context.Photographers.FindAsync(id);
        if (photographer == null)
            return NotFound();
        
        return View(photographer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Photographer photographer)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        if (id != photographer.Id)
            return NotFound();
        
        if (ModelState.IsValid)
        {
            _context.Update(photographer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(photographer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        var photographer = await _context.Photographers.FindAsync(id);
        if (photographer != null)
        {
            _context.Photographers.Remove(photographer);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}