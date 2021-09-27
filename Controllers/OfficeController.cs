using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OfficeController : Controller
    {
        public ActionResult Index()
        {

            OfficeLists model = new OfficeLists();
            var Office = new List<Office>()
            {
                new Office(1, "Our Office", "Tel Aviv-Yafo, Israel", 32.074902, 34.784700)
            };
            model.OfficeList = Office;
            return View(model);

        }
    }
}
