using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using OnlineExaminationSystem.Models;

namespace OnlineExaminationSystem.Controllers.admin
{
    public class ExamController : Controller
    {
        private OESContext db = new OESContext();

        // GET: /Exam/
        public ActionResult Index()
        {
            ViewBag.lessons = db.Lessons.ToList();
            return View(db.Lessons.ToList());
        }

        #region Get Exam
        [HttpPost]
        public JsonResult GetExams(JQueryDataTableParamModel param, string lessonName)
        {
            IQueryable<Exam> unfilteredData = db.Exams;
            IQueryable<Exam> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredData = unfilteredData.Where(c => (c.ExamName.Contains(param.sSearch)) ||
                                                         (c.ExamQuestionNumber.ToString().Contains(param.sSearch)) || (c.ExamTime.ToString().Contains(param.sSearch)));
            }
            else
            {
                filteredData = unfilteredData;
            }
            if (lessonName != "")
            {
                filteredData = filteredData.Where(w => w.Lesson.Id.ToString() == lessonName);
            }
            Expression<Func<Exam, long>> orderingFunction = (c => c.Id);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "desc")
            {
                filteredData = filteredData.OrderBy(orderingFunction);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(orderingFunction);
            }
            IEnumerable<Exam> displayData = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var result = from u in displayData
                select new object[]
                {
                    u.Id,
                    u.Lesson.LessonName,
                    u.ExamName,
                    u.ExamLectureName,
                    u.ExamQuestionNumber,
                    u.ExamTime,
                    "<button onclick=\"removeExam("+u.Id+", '"+u.ExamName+"');\" class='btn btn-danger btn-xs'><i class='glyphicon glyphicon-trash'></i> Sil</button> | "+
                    "<button onclick=\"editExam("+u.Id+", "+u.LessonId+", '"+u.ExamName+"', '"+u.ExamLectureName+"', "+u.ExamQuestionNumber+", "+u.ExamTime+");\" class='btn btn-primary btn-xs'><i class='glyphicon glyphicon-edit'></i> Düzenle</button>"
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
        public JsonResult CreateExam(Exam model, long LessonId)
        {
            if (db.Exams.Any(w => w.ExamName == model.ExamName))
            {
                ModelState.AddModelError("", "Girilen sınav adı sistemde zaten kayıtlıdır.");
            }
            if (model.LessonId == 0)
            {
                ModelState.AddModelError("", "Ders seçiniz!");
            }
            Lesson Lesson = null;
            if (LessonId != null)
            {
                Lesson = db.Lessons.Find(LessonId);
                if (Lesson == null)
                {
                    ModelState.AddModelError("", "Seçtiğiniz ders bulunamadı!");
                }
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return Json(new
                {
                    modelError = errors
                });
            }
            db.Exams.Add(new Exam
            {
                Id = model.Id,
                LessonId = model.LessonId,
                ExamName = model.ExamName,
                ExamLectureName = model.ExamLectureName,
                ExamQuestionNumber = model.ExamQuestionNumber,
                ExamTime = model.ExamTime
            });
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public JsonResult UpdateExam(Exam model)
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
            var exam = db.Exams.Find(model.Id);
            if (exam == null)
            {
                return Json(new
                {
                    modelError = "",
                    result = -1
                });
            }

            exam.LessonId = model.LessonId;
            exam.ExamName = model.ExamName;
            exam.ExamLectureName = model.ExamLectureName;
            exam.ExamQuestionNumber = model.ExamQuestionNumber;
            exam.ExamTime = model.ExamTime;
            db.Entry(exam).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public int DeleteExam(long id)
        {
            var exam = db.Exams.Find(id);
            if (exam == null)
            {
                return -1;
            }
            //TODO Kullanıcı ilişkisi var ise koşul eklenecek
            db.Exams.Remove(exam);
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
