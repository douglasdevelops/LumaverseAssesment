using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Engineer> Engineers { get; set; }
        DbSet<Location> Locations { get; set; }

        Task<int> SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
