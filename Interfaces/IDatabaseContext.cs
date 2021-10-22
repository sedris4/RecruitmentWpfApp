using Microsoft.EntityFrameworkCore;

using RecruitmentWpfApp.Models;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Interfaces
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<PersonData> People { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
        int SaveChanges();
    }
}