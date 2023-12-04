using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seedings
{
    // In ASP.NET Core, seeding refers to the process of populating the database with initial data
    // when the application is first initialized or when the database is created. 
    // In other words, as soon as the application is started, the database will be populated
    // But what if i want to populate the database when the database is created (see ./ApplicationDbContext)
    public class CourseSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDBContext>();

                if (!dbContext.Courses.Any())
                {
                    var initialData = new List<Course>
                    {
                        // Add more seed data as needed
                        new Course { CourseName = "course1" },
                        new Course { CourseName = "course2" },
                    };

                    dbContext.Courses.AddRange(initialData);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
