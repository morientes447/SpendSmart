using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly SpendsmartDbContext _context;

    public HomeController(ILogger<HomeController> logger, SpendsmartDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        //Akan menuju ke file Index yang berada di View
        return View();
    }

    public IActionResult Expenses()
    {
        //Mengambil semua data dari tabel Expenses dan kemudian disimpan ke dalam allExpenses dalam bentuk list "ToList()"
        var allExpenses = _context.Expenses.ToList();
        return View(allExpenses);
    }

    public IActionResult CreateEditExpense()
    {
        return View();
    }

    public IActionResult CreateEditExpenseForm(Expense model)
    {
        _context.Expenses.Add(model);
        _context.SaveChanges();

        //Akan menuju ke Index yang ada di bagian atas pada kode ini
        return RedirectToAction("Expenses");
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
