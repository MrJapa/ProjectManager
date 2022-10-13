using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static void Main(string[] Args)
    {
        printAllTasksAndTodods();
        printIncompleteTasksAndTodods();
    }
    public static void printAllTasksAndTodods()
    {
        Console.WriteLine("All tasks:");
        Console.WriteLine("_________");

        using (ProjectContext context = new ProjectContext())
        {
            var tasks = context.Tasks.Include(task => task.ToDo);
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Name}");
                foreach (var todo in task.ToDo)
                {
                    Console.WriteLine($"- {todo.Name}");
                }
            }
        }
        Console.WriteLine("______________________________");
    }

    public static void printIncompleteTasksAndTodods()
    {
        Console.WriteLine("All incomplete tasks and todos:");
        using (ProjectContext context = new ProjectContext())
        {
            var tasks = context.Tasks.Include(task => task.ToDo).Where(task => task.ToDo.Any(todo => todo.IsComplete == false));
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Name}");
                foreach (var todo in task.ToDo)
                {
                    Console.WriteLine($"- {todo.Name}");
                }
            }
        }
    }
    public static void seedTasks()
    {
        using var db = new ProjectContext();
        Task t1 = new Task();
        t1.Name = "Produce software";
        t1.ToDo.Add(new ToDo { Name = "Write code", IsComplete = false });
        t1.ToDo.Add(new ToDo { Name = "Compile source", IsComplete = false });
        t1.ToDo.Add(new ToDo { Name = "Test program", IsComplete = false });

        db.Add(t1);

        Task t2 = new Task();
        t2.Name = "Brew coffee";
        t2.ToDo.Add(new ToDo { Name = "Pour water",IsComplete = false });
        t2.ToDo.Add(new ToDo { Name = "Pour coffee", IsComplete = false });
        t2.ToDo.Add(new ToDo { Name = "Turn on", IsComplete = false });

        db.Add(t2);

        db.SaveChanges();
    }
}
public class ProjectContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<ToDo> ToDo { get; set; }

    public string DbPath { get; }

    public ProjectContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "ProjectManager.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Task
{
    public int TaskId { get; set; }
    public string Name { get; set; }

    public List<ToDo> ToDo { get; } = new();
}

public class ToDo
{
    public int ToDoId { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
}