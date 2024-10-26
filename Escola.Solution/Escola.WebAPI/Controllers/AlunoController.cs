using Microsoft.AspNetCore.Mvc;

namespace Escola.WebAPI.Controllers
{
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
