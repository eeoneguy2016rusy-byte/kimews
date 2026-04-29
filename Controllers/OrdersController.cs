using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;

namespace PhotoStudio.Controllers;

public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;
    public OrdersController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(int? serviceId = null, decimal? minPrice = null, decimal? maxPrice = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin" && userRole != "Photographer")
            return RedirectToAction("Login", "Account");

        var orders = _context.Orders
            .Include(o => o.Client)
            .Include(o => o.Photographer)
            .Include(o => o.Service)
            .AsQueryable();

        if (serviceId.HasValue)
        {
            orders = orders.Where(o => o.ServiceId == serviceId);
        }

        if (minPrice.HasValue)
        {
            orders = orders.Where(o => o.TotalAmount >= minPrice);
        }
        if (maxPrice.HasValue)
        {
            orders = orders.Where(o => o.TotalAmount <= maxPrice);
        }

        if (startDate.HasValue)
        {
            orders = orders.Where(o => o.SessionDate >= startDate);
        }
        if (endDate.HasValue)
        {
            orders = orders.Where(o => o.SessionDate.Date <= endDate.Value.Date);
        }

        ViewBag.Services = new SelectList(_context.Services, "Id", "ServiceName", serviceId);
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        return View(await orders.ToListAsync());
    }

    public IActionResult Create()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        ViewBag.Clients = new SelectList(_context.Clients, "Id", "FullName");
        ViewBag.Photographers = new SelectList(_context.Photographers, "Id", "FullName");
        ViewBag.Services = new SelectList(_context.Services, "Id", "ServiceName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        if (ModelState.IsValid)
        {
            var service = await _context.Services.FindAsync(order.ServiceId);
            if (service != null) order.TotalAmount = service.Price;

            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (userRole != "Admin")
            return RedirectToAction("Login", "Account");
        
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}