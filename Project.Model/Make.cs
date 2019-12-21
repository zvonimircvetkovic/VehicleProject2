using Project.Model.Common;
using System.Collections.Generic;

namespace Project.Model
{
    public class Make : IMake
    {
        public int Id { get; set; }
        public string Abrv { get; set; }
        public string Name { get; set; }
        public IEnumerable<IModel> Models { get; set; }
    }
}
