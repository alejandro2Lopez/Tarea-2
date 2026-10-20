using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarea_2.Models;
using Tarea_2.Models.Entities;

namespace Tarea_2.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Edit(int id)
        {
            return View();
        }

     
        [HttpPost]
        public ActionResult UpdateTask(Task task)
        {
           int result = TaskDts.GetInstanceTaskDts().updateTask(task);
            if (result == 1)
            {
                return Json(new
                {
                    status = 200,
                    message = "Se ha actualizado de manera exitosa"
                }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new
                {
                    status = 404,
                    message = "No se encontró tarea para actualizar"
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
           int isDelete = TaskDts.GetInstanceTaskDts().removeTask(id);

            if (isDelete>=1) { 
            return Json(new
            {
                status = 200,
                message = "Se ha borrado de manera exitosa"
            }, JsonRequestBehavior.AllowGet);
        }
            else
            {

                return Json(new
                {
                    status = 404,
                    lista = "No se encontró tarea para borrar"
                }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpGet]
        public JsonResult GetTaskListFilter(int id) {
            List<Task> taskList;
            if (id==0)
            {
                taskList = TaskDts.GetInstanceTaskDts().getTaskListFilter(true);

            }
            else {

                taskList = TaskDts.GetInstanceTaskDts().getTaskListFilter(false);
            }
            return Json(new
            {
                status = 200,
                list = taskList
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTaskList() {

            List<Task> taskList = TaskDts.GetInstanceTaskDts().getTaskList();

            return Json(new
            {
                status = 200,
                list = taskList
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTaskListById(int id)
        {

           Task task = TaskDts.GetInstanceTaskDts().getTaskById(id);

            return Json(new
            {
                status = 200,
                task = task
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTask(Task task)
        {
            TaskDts.GetInstanceTaskDts().addTask(task);
            return Json(new
            {
                status = 200,
                lista = "Tarea agregada de manera exitosa"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
