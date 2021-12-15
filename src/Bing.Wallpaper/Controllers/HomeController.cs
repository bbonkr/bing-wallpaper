
using Microsoft.AspNetCore.Mvc;

namespace Bing.Wallpaper.Controllers;

[Controller]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
