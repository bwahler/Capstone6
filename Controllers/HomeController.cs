using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone6.Models;
using System.Data.Entity;

namespace Capstone6.Controllers
{
    public class HomeController : Controller
    {
       

        public ActionResult Index()
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ViewBag.TaskList = ORM.TaskList.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddTask()
        {
            return View();
        }
        public ActionResult AddUser(Users newUser)
        {
            if (ModelState.IsValid)
            {
                TaskListDBEntities ORM = new TaskListDBEntities();
                ORM.Users.Add(newUser);
                ORM.SaveChanges();

                // go to task list, passing the new user to the taskList method
                return RedirectToAction("TaskList", newUser);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult SignIn(int UserID, string Email_Address, string Password)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            Users currentUser = ORM.Users.Find(UserID);
            if (currentUser.Email_Address == Email_Address && currentUser.Password == Password)
            {
                return RedirectToAction("TaskList", currentUser);
            }
            ViewBag.AuthError = "Email or password was incorrect. Try again";
            return View("Login");
        }
        public ActionResult SaveNewTask(TaskList newTask)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ORM.TaskList.Add(newTask);
            ORM.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(TaskList updateTask)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ORM.Entry(updateTask).State = EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MarkComplete(TaskList TaskID)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ORM.TaskList.Remove(ORM.TaskList.Find(TaskID));
            ORM.SaveChanges();
            return View("Index");
        }

        public ActionResult DeleteOrder(int TaskID)
        {
            // Initialize ORM
            TaskListDBEntities ORM = new TaskListDBEntities();

            

            // set var "found" to the Order of ID "OrderID"
            TaskList found = ORM.TaskList.Find(TaskID);

            // remember which user we're on (work-around until we actually learn how to use identity)
            Users currentUser = ORM.Users.Find(found.UserID);

            // remove that order from the database
            ORM.TaskList.Remove(found);

            // Always Be Saving
            ORM.SaveChanges();

            return RedirectToAction("TaskList", currentUser);
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult TaskList(Users currentUser)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();

            // viewbags to pass to the view
            ViewBag.UserName = currentUser.FirstName;
            ViewBag.UserID = currentUser.UserID;
            ViewBag.TaskList = ORM.TaskList.ToList();

            return View();
        }
    }
}