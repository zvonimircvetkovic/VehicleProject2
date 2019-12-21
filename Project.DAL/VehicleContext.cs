using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL
{
    public class VehicleContext : DbContext, IVehicleContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
        {
        }
        public DbSet<MakeEntity> Makes { get; set; }
        public DbSet<ModelEntity> Models { get; set; }
    }
}
