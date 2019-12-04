namespace Project.DAL.Entities
{
    public class ModelEntity : IModelEntity
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public virtual MakeEntity Make { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
