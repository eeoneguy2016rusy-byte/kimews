using Microsoft.EntityFrameworkCore;
using PhotoStudio.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=PhotoStudio.db"));
builder.Services.AddSession();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    context.Database.EnsureCreated();

    if (!context.Employees.Any())
    {
        context.Employees.Add(new Employee
        {
            FullName = "Admin",
            Email = "admin",
            Position = "Admin",
            Phone = "+7(123)456-78-90"
        });

        context.Employees.Add(new Employee
        {
            FullName = "Александр Волков",
            Email = "photographer",
            Position = "Photographer",
            Phone = "+7(987)654-32-10"
        });

        context.Employees.Add(new Employee
        {
            FullName = "Иван Петров",
            Email = "user",
            Position = "User",
            Phone = "+7(555)555-55-55"
        });

        context.SaveChanges();
    }

    if (!context.Services.Any())
    {
        context.Services.AddRange(new[]
        {
            new Service
            {
                ServiceName = "Портретная фотография",
                Description = "Профессиональные портреты в студии",
                Price = 3000,
                DurationMin = 60
            },
            new Service
            {
                ServiceName = "Свадебная фотосессия",
                Description = "Полный день свадебной фотографии",
                Price = 25000,
                DurationMin = 480
            },
            new Service
            {
                ServiceName = "Семейная фотография",
                Description = "Семейные снимки в студии или на природе",
                Price = 5000,
                DurationMin = 120
            },
            new Service
            {
                ServiceName = "Корпоративное мероприятие",
                Description = "Фотосъемка корпоративных событий",
                Price = 10000,
                DurationMin = 180
            },
            new Service
            {
                ServiceName = "Детская фотосессия",
                Description = "Веселые снимки детей любого возраста",
                Price = 2500,
                DurationMin = 45
            }
        });

        context.SaveChanges();
    }

    if (!context.Photographers.Any())
    {
        context.Photographers.AddRange(new[]
        {
            new Photographer
            {
                FullName = "Александр Волков",
                Email = "photographer@mail.ru",
                Phone = "+7(987)654-32-10",
                Specialization = "Портреты и свадьбы",
                ExperienceYears = 8
            },
            new Photographer
            {
                FullName = "Мария Сидорова",
                Email = "maria.sidorova@mail.ru",
                Phone = "+7(912)123-45-67",
                Specialization = "Детская и семейная фотография",
                ExperienceYears = 5
            },
            new Photographer
            {
                FullName = "Сергей Козлов",
                Email = "sergey.kozlov@mail.ru",
                Phone = "+7(921)987-65-43",
                Specialization = "Корпоративные события",
                ExperienceYears = 10
            }
        });

        context.SaveChanges();
    }

    if (!context.Clients.Any())
    {
        context.Clients.AddRange(new[]
        {
            new Client
            {
                FullName = "Дарья Морозова",
                Phone = "+7(901)111-22-33",
                Email = "daria.morozova@mail.ru",
                Notes = "Невеста на май 2026"
            },
            new Client
            {
                FullName = "Анна Лебедева",
                Phone = "+7(902)222-33-44",
                Email = "anna.lebedeva@mail.ru",
                Notes = "Семейная фотосессия"
            },
            new Client
            {
                FullName = "Павел Иванов",
                Phone = "+7(903)333-44-55",
                Email = "pavel.ivanov@mail.ru",
                Notes = "Корпоративный клиент"
            },
            new Client
            {
                FullName = "Елена Смирнова",
                Phone = "+7(904)444-55-66",
                Email = "elena.smirnova@mail.ru",
                Notes = "Портретная сессия"
            },
            new Client
            {
                FullName = "Олег Орлов",
                Phone = "+7(905)555-66-77",
                Email = "oleg.orlov@mail.ru",
                Notes = "Детская фотография"
            }
        });

        context.SaveChanges();
    }

    if (!context.Orders.Any())
    {
        var firstClient = context.Clients.First();
        var firstPhotographer = context.Photographers.First();
        var firstService = context.Services.First();

        context.Orders.AddRange(new[]
        {
            new Order
            {
                OrderDate = DateTime.Now,
                SessionDate = DateTime.Now.AddDays(7),
                Status = "Запланирован",
                TotalAmount = 3000,
                Notes = "Портретная сессия в студии",
                ClientId = firstClient.Id,
                PhotographerId = firstPhotographer.Id,
                ServiceId = firstService.Id
            },
            new Order
            {
                OrderDate = DateTime.Now.AddDays(-5),
                SessionDate = DateTime.Now.AddDays(14),
                Status = "Запланирован",
                TotalAmount = 25000,
                Notes = "Свадебная съемка",
                ClientId = context.Clients.Skip(1).First().Id,
                PhotographerId = firstPhotographer.Id,
                ServiceId = context.Services.Skip(1).First().Id
            },
            new Order
            {
                OrderDate = DateTime.Now.AddDays(-10),
                SessionDate = DateTime.Now.AddDays(21),
                Status = "Запланирован",
                TotalAmount = 5000,
                Notes = "Семейная фотография",
                ClientId = context.Clients.Skip(2).First().Id,
                PhotographerId = context.Photographers.Skip(1).First().Id,
                ServiceId = context.Services.Skip(2).First().Id
            }
        });

        context.SaveChanges();
    }
}

app.Run();