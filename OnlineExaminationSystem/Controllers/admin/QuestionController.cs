using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineExaminationSystem.Models;
using OnlineExaminationSystem;
using OnlineExaminationSystem.Models.ViewModel;

namespace OnlineExaminationSystem.Controllers.admin
{
    public class QuestionController : Controller
    {
        private OESContext db = new OESContext();

        // GET: /Question/
        public ActionResult Index()
        {
            //var questions = db.Questions.Include(q => q.Exam).Include(q => q.Lesson);
            MergedLessonExam model = new MergedLessonExam();
            model.LessonList = db.Lessons.ToList();
            model.ExamList = db.Exams.ToList();
            return View(model);
        }

        #region Get Questions
        [HttpPost]
        public JsonResult GetQuestions(JQueryDataTableParamModel param, string lessonName, string examName)
        {
            IQueryable<Question> unfilteredData = db.Questions;
            IQueryable<Question> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredData = unfilteredData.Where(c => (c.QuestionText.Contains(param.sSearch)) || (c.ChoiceA.ToString().Contains(param.sSearch)) || 
                                                         (c.ChoiceB.ToString().Contains(param.sSearch)) || (c.ChoiceC.ToString().Contains(param.sSearch)) ||
                                                         (c.ChoiceD.ToString().Contains(param.sSearch)) || (c.Answer.ToString().Contains(param.sSearch)));
            }
            else
            {
                filteredData = unfilteredData;
            }
            if (lessonName != "")
            {
                filteredData = filteredData.Where(w => w.Lesson.Id.ToString() == lessonName);
            }
            if (examName != "")
            {
                filteredData = filteredData.Where(w => w.Exam.Id.ToString() == examName);
            }

            Expression<Func<Question, long>> orderingFunction = (c => c.Id);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            filteredData = sortDirection == "desc" ? filteredData.OrderBy(orderingFunction) : filteredData.OrderByDescending(orderingFunction);
            IEnumerable<Question> displayData = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var result = from u in displayData
                select new object[]
                {
                    u.Id,
                    u.Lesson.LessonName,
                    u.Exam.ExamName,
                    u.QuestionText,
                    u.ChoiceA,
                    u.ChoiceB,
                    u.ChoiceC,
                    u.ChoiceD,
                    u.Answer,
                    "<button onclick=\"removeQuestion("+u.Id+", '"+u.QuestionText+"');\" class='btn btn-danger btn-xs'><i class='glyphicon glyphicon-trash'></i> Sil</button> | "+
                    "<button onclick=\"editQuestion("+u.Id+", "+u.LessonId+", "+u.ExamId+", '"+ u.QuestionText +"', '"+ u.ChoiceA +"', '"+ u.ChoiceB +"', '"+ u.ChoiceC +"', '"+ u.ChoiceD +"', '"+ u.Answer +"');\" class='btn btn-primary btn-xs'><i class='glyphicon glyphicon-edit'></i> Düzenle</button>"
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
        public JsonResult CreateQuestion(Question model)
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
            if (!string.IsNullOrEmpty(model.QuestionText))
            {
                if (db.Lessons.FirstOrDefault(u => u.LessonName == model.QuestionText) != null)
                {
                    return Json(new
                    {
                        modelError = "",
                        result = -1
                    });
                }
                db.Questions.Add(new Question
                {
                    Id = model.Id,
                    LessonId = model.LessonId,
                    ExamId = model.ExamId,
                    QuestionText = model.QuestionText,
                    ChoiceA = model.ChoiceA,
                    ChoiceB = model.ChoiceB,
                    ChoiceC = model.ChoiceC,
                    ChoiceD = model.ChoiceD,
                    Answer = model.Answer
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
        public JsonResult UpdateQuestion(Question model)
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
            var question = db.Questions.Find(model.Id);
            if (question == null)
            {
                return Json(new
                {
                    modelError = "",
                    result = -1
                });
            }

            question.Id = model.Id;
            question.LessonId = model.LessonId;
            question.ExamId = model.ExamId;
            question.QuestionText = model.QuestionText;
            question.ChoiceA = model.ChoiceA;
            question.ChoiceB = model.ChoiceB;
            question.ChoiceC = model.ChoiceC;
            question.ChoiceD = model.ChoiceD;
            question.Answer = model.Answer;
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public int DeleteQuestion(long id)
        {
            var question = db.Questions.Find(id);
            if (question == null)
            {
                return -1;
            }
            //TODO Lesson ilişkisi var ise koşul eklenecek
            db.Questions.Remove(question);
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
