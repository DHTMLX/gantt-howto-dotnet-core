using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace DHX.Gantt.Models
{
    public static class GanttSeeder
    {
        public static void Seed(GanttContext context)
        {

            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }


            using (var transaction = context.Database.BeginTransaction())
            {


                List<Task> tasks = new List<Task>()
                {
                    new Task()
                    {
                        Id = 1,
                        Text = "Project #2",
                        StartDate = DateTime.Today.AddDays(-3),
                        Duration = 18,
                        Progress = 0.4m,
                        ParentId = null,
                        SortOrder = 0
                    },
                    new Task()
                    {
                        Id = 2,
                        Text = "Task #1",
                        StartDate = DateTime.Today.AddDays(-2),
                        Duration = 8,
                        Progress = 0.6m,
                        ParentId = 1,
                        SortOrder = 1
                    },
                    new Task()
                    {
                        Id = 3,
                        Text = "Task #2",
                        StartDate = DateTime.Today.AddDays(-1),
                        Duration = 8,
                        Progress = 0.6m,
                        ParentId = 1,
                        SortOrder = 2
                    }
                };

                tasks.ForEach(s => context.Tasks.Add(s));
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Tasks ON;");
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Tasks OFF;");
                List<Link> links = new List<Link>()
                {
                    new Link() {Id = 1, SourceTaskId = 1, TargetTaskId = 2, Type = "1"},
                    new Link() {Id = 2, SourceTaskId = 2, TargetTaskId = 3, Type = "0"}
                };

                links.ForEach(s => context.Links.Add(s));
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Links ON;");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Links OFF;");
                transaction.Commit();
            }
        }
    }
}
