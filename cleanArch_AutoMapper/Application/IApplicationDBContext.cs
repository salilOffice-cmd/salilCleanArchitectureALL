using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IApplicationDBContext
    {
        DbSet<Course> Courses { get; }
        DbSet<Student> Students { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
