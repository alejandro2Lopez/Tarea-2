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
        private List<Task> taskList;

        private TaskDts()
        {
            taskList = new List<Task>();
            HttpContext.Current.Session["TaskList"] = taskList;
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
            taskList = HttpContext.Current.Session["TaskList"] as List<Task>;
            task.Id = taskList.Any() ? taskList.Max(taskC => taskC.Id) + 1: 1;
            taskList.Add(task);
            HttpContext.Current.Session["TaskList"] = taskList;

}
        public int removeTask(int id) {

            taskList = HttpContext.Current.Session["TaskList"] as List<Task>;

            int eliminados = taskList.RemoveAll( x => x.Id == id);

            HttpContext.Current.Session["TaskList"] = taskList;
            return eliminados;

        }

        public List <Task> getTaskList()
        {
            taskList = HttpContext.Current.Session["TaskList"] as List<Task>;

            return taskList;
        }
       
        public Task getTaskById(int id)
        {
            taskList = HttpContext.Current.Session["TaskList"] as List<Task>;

            return taskList.FirstOrDefault(task2 => task2.Id == id);
        }

        public int updateTask(Task task) {
            taskList = HttpContext.Current.Session["TaskList"] as List<Task>;
            Task task1 = taskList.FirstOrDefault( task2 => task2.Id == task.Id);
            if (task1 != null)
            {
                task1.Title = task.Title;
                task1.Description = task.Description;
                task1.Status = task.Status;
                HttpContext.Current.Session["TaskList"] = taskList;
                return 1;
            }
            else {
                return 0;
            }
        }
        public List<Task> getTaskListFilter(bool isPending)
        {
            taskList = HttpContext.Current.Session["TaskList"] as List<Task>;
            return taskList.Where(task => task.Status ==(!isPending?"Pendiente":"Completada")  ).ToList();

        }
    }
}