using C3_Controls.Models;
using System.Web.Mvc;

namespace C3_Controls.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Configuration()
        {
            return View();
        }

        public ActionResult ConfigureTowerLight()
        {
            return View(new TowerLight());
        }

        public ActionResult ConfigurePushToTest()
        {
            return View(new PushToTest());
        }
    }
}