namespace Project.Model.Common
{
    public interface IModel
    {
        string Abrv { get; set; }
        int Id { get; set; }
        IMake Make { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
    }
}