using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using OnlineExaminationSystem.Models;
using OnlineExaminationSystem.App_Start;
using OnlineExaminationSystem.Models.ViewModel;

namespace OnlineExaminationSystem.Controllers.admin
{
    public class LessonController : Controller
    {
        private OESContext db = new OESContext();

        // GET: /Lesson/
        public ActionResult Index()
        {
            return View(db.Lessons.ToList());
        }

        #region Get Lessons
        [HttpPost]
        public JsonResult GetLessons(JQueryDataTableParamModel param)
        {
            IQueryable<Lesson> unfilteredData = db.Lessons;
            IQueryable<Lesson> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredData = unfilteredData.Where(c => (c.LessonName.Contains(param.sSearch)));
            }
            else
            {
                filteredData = unfilteredData;
            }

            Expression<Func<Lesson, long>> orderingFunction = (c => c.Id);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "desc")
            {
                filteredData = filteredData.OrderBy(orderingFunction);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(orderingFunction);
            }
            IEnumerable<Lesson> displayData = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var result = from u in displayData
                select new object[]
                {
                    u.Id,
                    u.LessonName,
                    "<button onclick=\"removeLesson("+u.Id+", '"+u.LessonName+"');\" class='btn btn-danger btn-xs'><i class='glyphicon glyphicon-trash'></i> Sil</button> | "+
                    "<button onclick=\"editLesson("+u.Id+", '"+ u.LessonName +"');\" class='btn btn-primary btn-xs'><i class='glyphicon glyphicon-edit'></i> Düzenle</button>"
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
        public JsonResult CreateLesson(Lesson model)
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
            if (!string.IsNullOrEmpty(model.LessonName))
            {
                if (db.Lessons.FirstOrDefault(u => u.LessonName == model.LessonName) != null)
                {
                    return Json(new
                    {
                        modelError = "",
                        result = -1
                    });
                }
                db.Lessons.Add(new Lesson
                {
                    Id = model.Id,
                    LessonName = model.LessonName
                });
                db.SaveChanges();
                return Json(new
                {
                    modelError = "",
                    result = 1
                });
            }
            else
                return Json(new
                {
                    modelError = "",
                    result = 0
                });
        }

        [HttpPost]
        public JsonResult UpdateLesson(Lesson model)
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
            var lesson = db.Lessons.Find(model.Id);
            if (lesson == null)
            {
                return Json(new
                {
                    modelError = "",
                    result = -1
                });
            }

            lesson.Id = model.Id;
            lesson.LessonName = model.LessonName;
            db.Entry(lesson).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public int DeleteLesson(long id)
        {
            var lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return -1;
            }
            //TODO Lesson ilişkisi var ise koşul eklenecek
            db.Lessons.Remove(lesson);
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
