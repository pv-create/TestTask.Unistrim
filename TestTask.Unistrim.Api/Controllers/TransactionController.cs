using Microsoft.AspNetCore.Mvc;

namespace TestTask.Unistrim.Api.Controllers;

public class TransactionController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}