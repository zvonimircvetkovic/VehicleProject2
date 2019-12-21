using System.Collections.Generic;

namespace Project.DAL.Entities
{
    public interface IMakeEntity
    {
        string Abrv { get; set; }
        int Id { get; set; }
        IEnumerable<ModelEntity> ModelEntities { get; set; }
        string Name { get; set; }
    }
}