using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using OnlineExaminationSystem.App_Start;
using OnlineExaminationSystem.Models;
using OnlineExaminationSystem.Models.ViewModel;

namespace OnlineExaminationSystem.Controllers.admin
{
    public class ResultController : Controller
    {
        private static OESContext db = new OESContext();
        public ActionResult Index()
        {
            ViewBag.ExamList = db.Exams.ToList();
            return View(db.Exams.ToList());
        }

        #region Get Results
        [HttpPost]
        public JsonResult GetResults(JQueryDataTableParamModel param, string examId)
        {
            IQueryable<AssignedExam> unfilteredData = db.AssignedExams;
            IQueryable<AssignedExam> filteredData;
            if(!string.IsNullOrEmpty(param.sSearch))
            {
                filteredData = unfilteredData.Where(c => (c.User.Name.Contains(param.sSearch) || c.User.Surname.Contains(param.sSearch) || c.Exam.ExamName.Contains(param.sSearch)));
            }
            else
            {
                filteredData = unfilteredData;
            }
            if (examId != "")
            {
                filteredData = filteredData.Where(w => w.ExamId.ToString() == examId);
            }
            Expression<Func<AssignedExam, long>> orderingFunction = (c => c.Id);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            filteredData = sortDirection == "desc" ? filteredData.OrderBy(orderingFunction) : filteredData.OrderByDescending(orderingFunction);
            IEnumerable<AssignedExam> displayData = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var result = from u in displayData
                select new object[]
                {
                    u.Exam.ExamName,
                    u.User.Name + " " +u.User.Surname,
                    u.Exam.ExamQuestionNumber,
                    CalculateCorrectWrongAnswerCount(u.ExamId, u.UserID, true),
                    CalculateCorrectWrongAnswerCount(u.ExamId, u.UserID, false),
                    (CalculateCorrectWrongAnswerCount(u.ExamId, u.UserID, true) * 100) /u.Exam.ExamQuestionNumber
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

        public static int CalculateCorrectWrongAnswerCount(long examId, long userId, bool type)
        {
            int correctAnswer = 0;
            int wrongAnswer = 0;
            var questionList = db.Questions.Where(a => a.ExamId == examId).ToList();
            var answerList = db.Answers.Where(a => a.Question.ExamId == examId && a.UserID == userId).ToList();
            foreach (var question in answerList)
            {
                var x = questionList.Find(a => a.Id == question.QuestionId);
                if (x.Answer == question.UserAnswer)
                    correctAnswer++;
                else
                {
                    wrongAnswer++;
                }
            }
            if (type)
            {
                return correctAnswer;
            }
            return wrongAnswer;
        }
    }
}