using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;

namespace PhotoStudio.Controllers;

public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        var users = await _context.Employees.Where(e => e.Position == "User").ToListAsync();
        return View(users);
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
    public async Task<IActionResult> Create(Employee employee)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        employee.Position = "User";
        
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        var user = await _context.Employees.FindAsync(id);
        if (user != null && user.Position == "User")
        {
            _context.Employees.Remove(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
