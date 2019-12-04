namespace Project.DAL.Entities
{
    public interface IModelEntity
    {
        string Abrv { get; set; }
        int Id { get; set; }
        MakeEntity Make { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
    }
}