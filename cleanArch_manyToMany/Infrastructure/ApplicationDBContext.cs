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
        public DbSet<CourseStudent> CourseStudents => Set<CourseStudent>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring many to many relationships


            // This line says that the "CourseStudent" table will have a primary key
            // made up of both the "CourseId" and "StudentId" columns.
            // This is because in a many-to-many relationship,
            // the junction table needs a composite primary key to uniquely identify
            // each combination of course and student.
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new {cs.CourseId, cs.StudentId});


            // This line sets up the relationship between the "CourseStudent" and "Course" tables.
            // It says that each "CourseStudent" row will be related to one "Course",
            // and each "Course" can have many related "CourseStudent" rows.
            // It also specifies that the "CourseId" column in the "CourseStudent"
            // table will be the foreign key that links to the primary key
            // of the "Course" table.
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);



            // In simple terms, this code is just telling Entity Framework
            // how the "Course", "Student", and "CourseStudent" tables
            // are related to each other, so it knows how to handle the
            // many-to-many relationship between courses and students.
        }
    }
}
