using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using OnlineExaminationSystem.App_Start;
using OnlineExaminationSystem.Models;
using OnlineExaminationSystem.Models.ViewModel;

namespace OnlineExaminationSystem.Controllers.user
{
    public class UserHomeController : Controller
    {
        private OESContext db = new OESContext();
        //
        // GET: /UserHome/
        public ActionResult Index()
        {
            UserHomeViewModel model = new UserHomeViewModel
            {
                AssignedExamList = db.AssignedExams.ToList(),
                ExamList = db.Exams.ToList()
            };
            return View(model);
        }
        public ActionResult UserName()
        {
            string userName = "";
            if (Util.UserInfo != null)
                userName = Util.UserInfo.NameSurname;
            return Content(userName);
        }
        public ActionResult UserDepartment()
        {
            string department = "";
            if (Util.UserInfo != null)
                department = Util.UserInfo.Department;
            return Content(department);
        }
        public ActionResult StudentNumber()
        {
            long studentNumber = 0;
            if (Util.UserInfo != null)
                studentNumber = Util.UserInfo.StudentNumber;
            return Content(studentNumber.ToString());
        }
        public ActionResult ImageUrl()
        {
            string imageUrl = "";
            if (Util.UserInfo != null)
            {
                imageUrl = "/Content/images/" + Util.UserInfo.NameSurname + ".jpg";
                imageUrl = imageUrl.Replace(" ", "");
            }
            var filePath = Server.MapPath(Url.Content("~"+imageUrl));
            if (!System.IO.File.Exists(filePath))
            {
                imageUrl = "/Content/images/Erkek.png";
            }
            return Content(imageUrl);
        }
	}
}