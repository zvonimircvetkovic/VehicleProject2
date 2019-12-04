using System.Collections.Generic;

namespace Project.DAL.Entities
{
    public class MakeEntity : IMakeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual List<ModelEntity> ModelEntities { get; set; }
    }
}
