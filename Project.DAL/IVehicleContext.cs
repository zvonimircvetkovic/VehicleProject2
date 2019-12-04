using Microsoft.EntityFrameworkCore;
using Project.DAL.Abstract;
using Project.DAL.Entities;

namespace Project.DAL
{
    public interface IVehicleContext : IDbContext
    {
        DbSet<MakeEntity> Makes { get; set; }
        DbSet<ModelEntity> Models { get; set; }
    }
}