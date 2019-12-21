using System.Collections.Generic;

namespace Project.Model.Common
{
    public interface IMake
    {
        string Abrv { get; set; }
        int Id { get; set; }
        IEnumerable<IModel> Models { get; set; }
        string Name { get; set; }
    }
}