using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>().HasData(
                    new Course { CourseId = 1, CourseName = "course1", CreatedBy = "Server", CreatedDate = DateTime.Now, IsActive = true },
                    new Course { CourseId = 2, CourseName = "course2", CreatedBy = "Server", CreatedDate = DateTime.Now, IsActive = true }
            );
            // Note : When using the HasData() to seed data,
            // you need to ensure that the primary key (identity property) is explicitly provided
            // in the seed data. If the primary key is not explicitly provided, you will encounter an error.
        }
    }
}
