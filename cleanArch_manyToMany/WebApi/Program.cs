using Application;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Registering dependency for automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Creating the context object
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.
        Configuration.GetConnectionString("DefaultConnectionString"),
        builder => builder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName))
    );


// Dependency injection for the context object
builder.Services.AddScoped<IApplicationDBContext>(
    provider => provider.GetRequiredService<ApplicationDBContext>()
);

// service for type 'MediatR.ISender' has been registered.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();


//Select Students.StudentID,
//       Students.StudentName,
//       CourseStudents.CourseId,
//       Courses.CourseName
//From   CourseStudents
//Join   Students
//on     Students.StudentID = CourseStudents.StudentId
//Join   Courses
//on     Courses.CourseId = CourseStudents.CourseId
//Order by Students.StudentID