using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskListApplication.Server.Data;

namespace TaskListApplication.Tests
{
    public static class TestUtility
    {
        public static TaskContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new TaskContext(options);
            context.Database.EnsureCreated();
            return context;
        }

    }
}
