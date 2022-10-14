using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static void Main(string[] Args)
    {
        seedTasksAndTeams();
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
    public static void seedWorkers()
    {
        var Frontend = new Team("Frontend");
        var Backend = new Team("Backend");
        var Testere = new Team("Testere");

        using (ProjectContext db = new ProjectContext())
        {
            //Frontend
            db.TeamWorker.Add(new TeamWorker { Team = Frontend, Worker = new Worker { Name = "Steen Secher" } });
            db.TeamWorker.Add(new TeamWorker { Team = Frontend, Worker = new Worker { Name = "Ejvind Møller" } });
            db.TeamWorker.Add(new TeamWorker { Team = Frontend, Worker = new Worker { Name = "Konrad Sommer" } });

            //Backend
            db.TeamWorker.Add(new TeamWorker { Team = Backend, Worker = new Worker { Name = "Konrad Sommer" } });
            db.TeamWorker.Add(new TeamWorker { Team = Backend, Worker = new Worker { Name = "Sofus Lotus" } });
            db.TeamWorker.Add(new TeamWorker { Team = Backend, Worker = new Worker { Name = "Remo Lademann" } });

            //Testere
            db.TeamWorker.Add(new TeamWorker { Team = Testere, Worker = new Worker { Name = "Ella Fanth" } });
            db.TeamWorker.Add(new TeamWorker { Team = Testere, Worker = new Worker { Name = "Anne Dam" } });
            db.TeamWorker.Add(new TeamWorker { Team = Testere, Worker = new Worker { Name = "Steen Secher" } });
            db.SaveChanges();
        };


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
        t2.ToDo.Add(new ToDo { Name = "Pour water", IsComplete = false });
        t2.ToDo.Add(new ToDo { Name = "Pour coffee", IsComplete = false });
        t2.ToDo.Add(new ToDo { Name = "Turn on", IsComplete = false });

        db.Add(t2);

        db.SaveChanges();
    }
    public static void seedTasksAndTeams()
    {
        using var db = new ProjectContext();
        Team t1 = new Team();
        t1.Name = "New team";

        Task t2 = new Task();
        t2.Name = "New Task";
        t2.ToDo.Add(new ToDo { Name = "New Todo", IsComplete = true });
        t1.Tasks.Add(t2);
        db.Add(t1);

        db.SaveChanges();

    }
}
public class ProjectContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<ToDo> ToDo { get; set; }
    public DbSet<Team>? Team { get; set; }
    public DbSet<Worker>? Worker { get; set; }
    public DbSet<TeamWorker>? TeamWorker { get; set; }



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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamWorker>()
            .HasKey(p => new { p.TeamId, p.WorkerId });

        modelBuilder.Entity<Team>()
            .HasOne(team => team.CurrentTask);

        modelBuilder.Entity<Worker>()
            .HasOne(team => team.CurrentTodo);
    }
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

public class Team
{
    public int TeamId { get; set; }
    public string Name { get; set; }
    public List<TeamWorker> Workers { get; } = new();
    public Task? CurrentTask { get; set; }
    public List<Task> Tasks { get; set; } = new();
    public Team()
    {

    }
    public Team(string name)
    {
        Name = name;
    }
}

public class Worker
{
    public int WorkerId { get; set; }
    public string Name { get; set; }
    public List<TeamWorker> Teams { get; } = new();
    public ToDo? CurrentTodo { get; set; }
    public List<ToDo> ToDos { get; set; } = new();
}

public class TeamWorker
{
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public int WorkerId { get; set; }
    public Worker? Worker { get; set; }
}