using System.Web;
using System.Web.Optimization;

namespace OnlineExaminationSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include(
                "~/Scripts/jquery-2.2.3.min.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/fastclick.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/app.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/jquery.alerts.js",
                "~/Scripts/spin.js",
                "~/ScriptsCustom/Common/appInit.js",
                "~/ScriptsCustom/Common/dtLanguage.js",
                "~/ScriptsCustom/Common/messages.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css_bundle").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/site.css",
                "~/Content/css/ionicons.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/dataTables.bootstrap.css",
                "~/Content/css/custom.css",
                "~/Content/css/jquery.alerts.css",
                "~/Content/css/ionicons.css"
            ));

            #region User
            bundles.Add(new ScriptBundle("~/Scripts/userconfig").Include(
                "~/ScriptsCustom/Admin/User/initTableUser.js",
                "~/ScriptsCustom/Admin/User/removeUser.js",
                "~/ScriptsCustom/Admin/User/editUser.js",
                "~/ScriptsCustom/Admin/User/addUser.js"
            ));
            #endregion

            #region Lesson
            bundles.Add(new ScriptBundle("~/Scripts/lesson").Include(
                "~/ScriptsCustom/Admin/Lesson/addLesson.js",
                "~/ScriptsCustom/Admin/Lesson/editLesson.js",
                "~/ScriptsCustom/Admin/Lesson/initTableLesson.js",
                "~/ScriptsCustom/Admin/Lesson/removeLesson.js"
            ));
            #endregion

            #region Exam
            bundles.Add(new ScriptBundle("~/Scripts/exam").Include(
                "~/ScriptsCustom/Admin/Exam/addExam.js",
                "~/ScriptsCustom/Admin/Exam/editExam.js",
                "~/ScriptsCustom/Admin/Exam/initTableExam.js",
                "~/ScriptsCustom/Admin/Exam/removeExam.js"
            ));
            #endregion

            #region Assign Exam
            bundles.Add(new ScriptBundle("~/Scripts/assignExam").Include(
                "~/ScriptsCustom/Admin/AssignExam/addAssignExam.js",
                "~/ScriptsCustom/Admin/AssignExam/editAssignExam.js",
                "~/ScriptsCustom/Admin/AssignExam/initTableAssignExam.js",
                "~/ScriptsCustom/Admin/AssignExam/removeAssignExam.js"
            ));
            #endregion

            #region Question
            bundles.Add(new ScriptBundle("~/Scripts/question").Include(
                "~/ScriptsCustom/Admin/Question/addQuestion.js",
                "~/ScriptsCustom/Admin/Question/editQuestion.js",
                "~/ScriptsCustom/Admin/Question/initTableQuestion.js",
                "~/ScriptsCustom/Admin/Question/removeQuestion.js"
            ));
            #endregion

            #region Result
            bundles.Add(new ScriptBundle("~/Scripts/result").Include(
                "~/ScriptsCustom/Admin/Result/initTableResult.js"
            ));
            #endregion

            #region User Exam
            bundles.Add(new ScriptBundle("~/Scripts/userExam").Include(
                "~/ScriptsCustom/User/UserExam/startExam.js",
                "~/ScriptsCustom/User/UserExam/finishExam.js",
                "~/ScriptsCustom/User/UserExam/answerPaper.js",
                "~/ScriptsCustom/User/UserExam/nextQuestion.js",
                "~/ScriptsCustom/User/UserExam/previousQuestion.js",
                "~/Scripts/bootstrap3-wysihtml5.all.min.js"
            ));
            #endregion

            #region Dashboard
            bundles.Add(new ScriptBundle("~/Scripts/dashboard").Include(
                "~/ScriptsCustom/Admin/Dashboard/createChart.js",
                "~/ScriptsCustom/Admin/Dashboard/displayGraph.js",
                "~/ScriptsCustom/Admin/Dashboard/canvasjs.min.js"
            ));
            #endregion

        }
    }
}
