using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExaminationSystem.App_Start;
using OnlineExaminationSystem.Models.ViewModel;

namespace OnlineExaminationSystem.Controllers.admin
{
    public class AdminHomeController : Controller
    {
        private OESContext db = new OESContext();
        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel
            {
                ExamList = db.Exams.ToList()
            };
            return View(model);
        }
        public JsonResult GetGraphData(long id)
        {
            return Json(new
            {
                //TODO Fevzi
            });
        }
        public ActionResult UserName()
        {
            string userName = "";
            if (Util.UserInfo != null)
                userName = Util.UserInfo.NameSurname;
            return Content(userName);
        }
	}
}