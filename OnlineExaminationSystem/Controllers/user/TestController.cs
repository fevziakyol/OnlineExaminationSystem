using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using OnlineExaminationSystem.App_Start;
using OnlineExaminationSystem.Models;
using OnlineExaminationSystem.Models.ViewModel;

namespace OnlineExaminationSystem.Controllers.user
{
    public class TestController : Controller
    {
        private OESContext db = new OESContext();
        public ActionResult Index(long id, int questionNumber)
        {
            var assignedExam = db.AssignedExams.ToList().Find(a => a.UserID == Util.UserInfo.UserID && a.ExamId == id);
            if (assignedExam != null && assignedExam.ExamStartDate == null)
            {
                assignedExam.ExamStartDate = DateTime.Now;
                db.SaveChanges();
            }
            TestViewModel model = new TestViewModel
            {
                QuestionList = db.Questions.Where(a => a.Exam.Id == id).ToList(),
                Question = db.Questions.Where(a => a.Exam.Id == id).ToList().ElementAt(questionNumber-1),
                Exam = db.Exams.ToList().Find(a => a.Id == id)
            };
            var answer = db.Answers.ToList().Find(a => a.QuestionId == model.Question.Id && a.UserID == Util.UserInfo.UserID);
            ViewBag.selectedAnswer = "";
            if (answer != null)
            {
                ViewBag.selectedAnswer = answer.UserAnswer;
            }
            var endDate = assignedExam.ExamStartDate.Value.AddMinutes(model.Exam.ExamTime);
            double minutes = (endDate - DateTime.Now).TotalMinutes;
            double seconds = (endDate - DateTime.Now).Seconds;
            ViewBag.RemainingTimeMinute = (int)minutes;
            ViewBag.RemainingTimeSecond = (int)seconds;
            return View(model);
        }
        public JsonResult SetAnswer(Answer model)
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
            var answer = db.Answers.ToList().Find(a => a.UserID == Util.UserInfo.UserID && a.QuestionId == model.QuestionId);
            if (answer != null)
            {
                answer.UserID = Util.UserInfo.UserID;
                answer.QuestionId = model.QuestionId;
                answer.UserAnswer = model.UserAnswer;
                answer.Date = DateTime.Now;
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new
                {
                    modelError = "",
                    result = 1
                });
            }
            db.Answers.Add(new Answer
            {
                Id = model.Id,
                UserID = Util.UserInfo.UserID,
                QuestionId = model.QuestionId,
                UserAnswer = model.UserAnswer,
                Date = DateTime.Now
            });
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }
        public JsonResult FinishAssignExam(AssignedExam model)
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
            var assignedExam = db.AssignedExams.ToList().Find(a => a.ExamId == model.ExamId && a.UserID == Util.UserInfo.UserID);
            if (assignedExam == null)
            {
                return Json(new
                {
                    modelError = "",
                    result = -1
                });
            }

            assignedExam.UserID = Util.UserInfo.UserID;
            assignedExam.ExamId = model.ExamId;
            assignedExam.IsActiv = false;
            db.Entry(assignedExam).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }
        public JsonResult GetAnswers(JQueryDataTableParamModel param, int examId)
        {
            IEnumerable<Answer> displayData = db.Answers.Where(a => a.UserID == Util.UserInfo.UserID && a.Question.ExamId == examId).ToList();
            var counter = 1;
            var result = from u in displayData
                select new object[]
                {
                    counter++,
                    u.UserAnswer
                };
            return Json(new
            {
                param.sEcho,
                iTotalRecords = displayData.Count(),
                iTotalDisplayRecords = displayData.Count(),
                aaData = result
            });
        }
    }
}