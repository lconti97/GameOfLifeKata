using System.Web.Mvc;

namespace GameOfLifeKata.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}