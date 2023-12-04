using Application;
using Application.DTOs.CourseDTOs;
using Application.Validators;
using Domain.Entities;
using FluentValidation;
using Infrastructure;
using Infrastructure.Seedings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





// Creating the context object
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.
        Configuration.GetConnectionString("DefaultConnectionString"),
        builder => builder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)),
        ServiceLifetime.Transient
    );


// Dependency injection for the context object
builder.Services.AddScoped<IApplicationDBContext>(
    provider => provider.GetRequiredService<ApplicationDBContext>()
);



// Service for type 'MediatR.ISender' has been registered.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});



// Register Validator 
builder.Services.AddScoped<IValidator<AddCourseDTO>, CourseValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<CourseValidator>();


var app = builder.Build();



// Adding Seeding data
// The data should be inserted to the database only after building the application
// That's why this line needs to be written here
CourseSeeder.SeedData(app.Services);



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
