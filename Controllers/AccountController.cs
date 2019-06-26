using System;
using System.Linq;
using EsisaSurvey.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EsisaSurvey.CoreStoreBE;

namespace EsisaSurvey.Controllers
{
    public class AccountController : Controller
    {
        private EfCore _context;

        public AccountController()
        {
            _context = new EfCore();
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Client_login()
        {
            return View();
        }

        public ActionResult Validate(string email, string password)
        {
            try
            {
                var query = _context.Commercant.SingleOrDefault(c => c.login == email && c.password == password);
                HttpContext.Session.SetString("Commercantid", query.commercantId.ToString());
                Commercant commercant = query;
                int commercantId = query.commercantId;
                var commercantname = query.commercantName;
                var commercantcompany = query.Entreprise;
                return Json(new { status = true, message = "Success!",commercantname,commercantcompany });
            }
            catch (Exception e)
            {
                return Json(new { status = false, message = "Invalid login! please try again"  });
            }
        }


        public ActionResult ValidateClient(string email, string password)
        {
           try
            {

                var query = _context.Client.SingleOrDefault(c => c.login == email && c.password == password);
                HttpContext.Session.SetString("Clientid", query.clientid.ToString());
                Client client = query;
                int commercantId = query.clientid;
                string login = client.login;
                string name = client.name;
                return Json(new { status = true, message = "Success!" ,login,name});
            }
            catch (Exception e)
            {
                return Json(new { status = false, message = "Invalid Login please try again!" });
            }
        }
    }
}