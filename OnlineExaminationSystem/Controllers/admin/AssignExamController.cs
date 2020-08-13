using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using OnlineExaminationSystem.App_Start;
using OnlineExaminationSystem.Models;

namespace OnlineExaminationSystem.Controllers.admin
{
    public class AssignExamController : Controller
    {
        private OESContext db = new OESContext();
        // GET: AssignExam
        public ActionResult Index()
        {
            ViewBag.assignedExamList = db.AssignedExams.ToList();
            ViewBag.userList = db.Users.ToList();
            ViewBag.examList = db.Exams.ToList();
            return View();
        }

        #region Get Exam
        [HttpPost]
        public JsonResult GetAssignExams(JQueryDataTableParamModel param, string examName, string userName)
        {
            IQueryable<AssignedExam> unfilteredData = db.AssignedExams;
            IQueryable<AssignedExam> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredData = unfilteredData.Where(c => (c.Exam.ExamName.Contains(param.sSearch)) ||
                                                         (c.User.Name.Contains(param.sSearch)));
            }
            else
            {
                filteredData = unfilteredData;
            }
            if (examName != "")
            {
                filteredData = filteredData.Where(w => w.Exam.Id.ToString() == examName);
            }
            if (userName != "")
            {
                filteredData = filteredData.Where(w => w.User.ID.ToString() == userName);
            }
            Expression<Func<AssignedExam, long>> orderingFunction = (c => c.Id);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "desc")
            {
                filteredData = filteredData.OrderBy(orderingFunction);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(orderingFunction);
            }
            IEnumerable<AssignedExam> displayData = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var result = from u in displayData
                select new object[]
                {
                    u.Id,
                    u.User.Name + " " + u.User.Surname,
                    u.Exam.ExamName,
                    u.IsActiv ? "Aktif": "Pasif",
                    "<button onclick=\"removeAssignExam("+u.Id+", '"+u.User.Name+"', '"+u.Exam.ExamName+"');\" class='btn btn-danger btn-xs'><i class='glyphicon glyphicon-trash'></i> Sil</button> | "+
                    "<button onclick=\"editAssignExam("+u.Id+", "+u.User.ID+", "+u.Exam.Id+", '"+u.IsActiv+"');\" class='btn btn-primary btn-xs'><i class='glyphicon glyphicon-edit'></i> Düzenle</button>"
                };
            return Json(new
            {
                param.sEcho,
                iTotalRecords = unfilteredData.Count(),
                iTotalDisplayRecords = filteredData.Count(),
                aaData = result
            });
        }
        #endregion

        [HttpPost]
        public JsonResult CreateAssignExam(AssignedExam model)
        {
            if (db.AssignedExams.Any(w => w.Exam.Id == model.ExamId && w.User.ID == model.UserID))
            {
                ModelState.AddModelError("", "Girmek istediğiniz sınav ataması sistemde zaten kayıtlıdır.");
            }
            if (model.ExamId == 0)
            {
                ModelState.AddModelError("", "Sınav seçiniz!");
            }
            if (model.UserID == 0)
            {
                ModelState.AddModelError("", "Kullanıcı seçiniz!");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(new
                {
                    modelError = errors
                });
            }
            db.AssignedExams.Add(new AssignedExam()
            {
                Id = model.Id,
                UserID = model.UserID,
                ExamId = model.ExamId,
                IsActiv = model.IsActiv
            });
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public JsonResult UpdateAssignExam(AssignedExam model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(new
                {
                    modelError = errors,
                    result = 1
                });
            }
            var assignedExam = db.AssignedExams.Find(model.Id);
            if (assignedExam == null)
            {
                return Json(new
                {
                    modelError = "",
                    result = -1
                });
            }

            assignedExam.UserID = model.UserID;
            assignedExam.ExamId = model.ExamId;
            assignedExam.IsActiv = model.IsActiv;
            db.Entry(assignedExam).State = EntityState.Modified;
            var exam = db.AssignedExams.ToList().Find(a => a.ExamId == model.ExamId && a.UserID == model.UserID);
            exam.ExamStartDate = null;
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public int DeleteAssignExam(long id)
        {
            var assignedExam = db.AssignedExams.Find(id);
            if (assignedExam == null)
            {
                return -1;
            }
            //TODO Kullanıcı ilişkisi var ise koşul eklenecek
            db.AssignedExams.Remove(assignedExam);
            db.SaveChanges();
            return 1;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}