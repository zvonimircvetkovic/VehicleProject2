using Project.Model.Common;

namespace Project.Model
{
    public class Model : IModel
    {
        public string Abrv { get; set; }
        public int Id { get; set; }
        public IMake Make { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
    }
}
