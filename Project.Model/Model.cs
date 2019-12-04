using Project.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Model : IModel
    {
        [Required]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
        public int Id { get; set; }
        public IMake Make { get; set; }
        [Required]
        public int MakeId { get; set; }
        [Required]
        [Display(Name = "Model Name")]
        public string Name { get; set; }
    }
}
