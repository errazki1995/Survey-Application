using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EsisaSurvey.CoreStoreBE;
using EsisaSurvey.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace EsisaSurvey.Controllers
{
    
    [Route("")]
    public class ServiceController : Controller
    {
        public EfCore _context;
        public ServiceController()
        {
            _context = new EfCore();
        }

    [HttpGet("getActiveClient")]
        public JsonResult GetActiveClient()
        {
            int clientid = Convert.ToInt32(HttpContext.Session.GetString("Clientid"));
            var db = new Dao(HttpContext.Session);
            return Json(db.GetAllSurveys());
        }


        [HttpGet("getActive")]
        public JsonResult GetActive()
        {
            int comid = Convert.ToInt32(HttpContext.Session.GetString("Commercantid"));

            var db = new Dao(HttpContext.Session, comid);
            return Json(db.GetSurveys());
        }

        [HttpGet("getSurvey")]
        public string GetSurvey(string surveyId)
        {
            int comid = Convert.ToInt32(HttpContext.Session.GetString("Commercantid"));

            var db = new Dao(HttpContext.Session, comid);
            return db.GetSurvey(surveyId);
        }

        [HttpGet("create")]
        public JsonResult Create(string name)
        {
            int comid = Convert.ToInt32(HttpContext.Session.GetString("Commercantid"));

            var db = new Dao(HttpContext.Session, comid);
            Debug.Write("THE USERRRRRRR ISSSSS:" + comid);
            db.StoreSurvey(name, "{}", comid);
            return Json("Ok");
        }

        [HttpGet("changeName")]
        public JsonResult ChangeName(string id, string name)
        {
            int comid = Convert.ToInt32(HttpContext.Session.GetString("Commercantid"));

            var db = new Dao(HttpContext.Session, comid);
            db.ChangeName(id, name);
            return Json("Ok");
        }

        [HttpPost("changeJson")]
        public string ChangeJson([FromBody]Survey model)
        {
            int comid = Convert.ToInt32(HttpContext.Session.GetString("Commercantid"));

            var db = new Dao(HttpContext.Session, comid);
            db.StoreSurvey(model.Id, model.Json, comid);
            return db.GetSurvey(model.Id);
        }

        [HttpGet("delete")]
        public JsonResult Delete(string id)
        {
            int comid = Convert.ToInt32(HttpContext.Session.GetString("Commercantid"));

            var db = new Dao(HttpContext.Session, comid);
            db.DeleteSurvey(id);
            return Json("Ok");
        }


        [HttpPost("post")]
        public JsonResult PostResult([FromBody]PostSurveyResultModel model)
        {
            SurveyResultLog surveyLog = new SurveyResultLog();
            int clientid = Convert.ToInt32(HttpContext.Session.GetString("Clientid"));
            var db = new Dao(HttpContext.Session);
            db.PostResults(model.postId, model.surveyResult, clientid);
            return Json("Ok");
        }
        [HttpGet("results")]
        public JsonResult GetResults(string postId)
        {
            
            var db = new Dao(HttpContext.Session);
            return Json(db.GetResults(postId));
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
