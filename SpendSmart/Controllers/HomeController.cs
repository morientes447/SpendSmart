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
        var totalExpenses = allExpenses.Sum(x => x.Value);

        ViewBag.Expenses = totalExpenses;
        return View(allExpenses);
    }

    public IActionResult CreateEditExpense(int? id)
    {
        if(id != null)
        {
            //Jika id tidak null, maka akan mengedit
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expenseInDb);
        }
        //Jika id null, maka akan membuat baru
        return View();
    }

    public IActionResult DeleteExpense(int id)
    {
        //Mencari satu data dari tabel Expenses yang memiliki Id yang sesuai dengan parameter id
        var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
        _context.Expenses.Remove(expenseInDb);
        _context.SaveChanges();
        //setelah menghapus sesuatu, kita mau user kembali ke page Overview/Expenses
        return RedirectToAction("Expenses");
    }

    public IActionResult CreateEditExpenseForm(Expense model)
    {
        if (model.Id == 0)
        {
            //Kalau tidak ada id yang cocok, kita create
            //Add berguna untuk membuat Id secara otomatis dan incremental
            _context.Expenses.Add(model);

        }
        else
        {
            //Kalau id ada, kita edit
            _context.Expenses.Update(model);
        }

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
