using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

using System.Web;
using System.Web.Mvc;
using Tarea_2.Models.Entities;
namespace Tarea_2.Models
{
    public class TaskDts
    {
        private static TaskDts instanceTaskDts;
        private List<Task> listTask;

        private TaskDts()
        {
            listTask = new List<Task>();

        }

        public static TaskDts GetInstanceTaskDts()
        {
            if (instanceTaskDts == null)
            {
                instanceTaskDts = new TaskDts();

            }

            return instanceTaskDts;

        }

        public void addTask(Task task){

            listTask.Add(task);

            HttpContext.Current.Session["TaskList"] = listTask;

}
        public int removeTask(int id) {

            listTask = HttpContext.Current.Session["TaskList"] as List<Task>;

            int eliminados = listTask.RemoveAll( x => x.Id == id);

            HttpContext.Current.Session["TaskList"] = listTask;
            return eliminados;

        }

        public List <Task> getTaskList()
        {
            listTask = HttpContext.Current.Session["TaskList"] as List<Task>;

            return listTask;
        }

        public int updateTask(int id, Task task) {
            listTask = HttpContext.Current.Session["TaskList"] as List<Task>;
            Task task1 = listTask.FirstOrDefault( task2 => task2.Id == id);
            if (task1 != null)
            {
                task1.Title = task.Title;
                task1.Description = task.Description;
                task.Status = task1.Status;
                HttpContext.Current.Session["TaskList"] = listTask;
                return 1;
            }
            else {
                return 0;
            }
        }
        public List<Task> getTaskListFilter(bool isPending)
        {
            listTask = HttpContext.Current.Session["TaskList"] as List<Task>;
            return listTask.Where(task => task.Status ==(isPending?"Pendiente":"Completado")  ).ToList();

        }
    }
}