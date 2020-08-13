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
    public class UserController : Controller
    {
        private OESContext db = new OESContext();

        // GET: /User/
        public ActionResult Index()
        {
            ViewBag.IsAdmin = "visible";
            return View(db.Users.ToList());
        }

        #region Get Users
        [HttpPost]
        public JsonResult GetUsers(JQueryDataTableParamModel param)
        {
            IQueryable<User> unfilteredData = db.Users;
            IQueryable<User> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredData = unfilteredData.Where(c => (c.Name.Contains(param.sSearch)) ||
                    (c.Surname.ToString().Contains(param.sSearch)) || (c.Email.ToString().Contains(param.sSearch)));
            }
            else
            {
                filteredData = unfilteredData;
            }

            Expression<Func<User, long>> orderingFunction = (c => c.ID);
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "desc")
            {
                filteredData = filteredData.OrderBy(orderingFunction);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(orderingFunction);
            }
            IEnumerable<User> displayData = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var result = from u in displayData
                         select new object[]
                    {
                        u.Name,
                        u.Surname,
                        u.StudentNumber,
                        u.Department,
                        u.Email,
                        "<button onclick=\"removeUser("+u.ID+", '"+u.Email+"');\" class='btn btn-danger btn-xs'><i class='glyphicon glyphicon-trash'></i> Sil</button> | "+
                        "<button onclick=\"editUser("+u.ID+", '"+ u.Name +"', '"+u.Surname+"', '"+u.StudentNumber+"', '"+u.Department+"', '"+u.Email+"', '"+u.Password+"');\" class='btn btn-primary btn-xs'><i class='glyphicon glyphicon-edit'></i> Düzenle</button>"
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
        public JsonResult CreateUser(UserViewModel model)
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
            if (!string.IsNullOrEmpty(model.UserName))
            {
                if (db.Users.FirstOrDefault(u => u.Email == model.UserName) != null)
                {
                    return Json(new
                    {
                        modelError = "",
                        result = -1
                    });
                }
                db.Users.Add(new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    StudentNumber = model.StudentNumber,
                    Department = model.Department,
                    Email = model.UserName,
                    Password = Util.GetMd5Hash(model.Password)

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
        public JsonResult UpdateUser(UserViewModel model)
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
            var user = db.Users.Find(model.ID);
            if (user == null)
            {
                return Json(new
                {
                    modelError = "",
                    result = -1
                });
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.StudentNumber = model.StudentNumber;
            user.Department = model.Department;
            user.Email = model.UserName;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            if (user.ID == Util.UserInfo.UserID)
            {
                Util.UserInfo.NameSurname = model.Name + " " + model.Surname;
            }
            return Json(new
            {
                modelError = "",
                result = 1
            });
        }

        [HttpPost]
        public int DeleteUser(long id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return -1;
            }
            //TODO Kullanıcı ilişkisi var ise koşul eklenecek
            db.Users.Remove(user);
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
