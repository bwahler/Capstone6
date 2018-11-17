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
                return View("Index");
            }
            else
            {
                return View("Error");
            }
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
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
    }
}