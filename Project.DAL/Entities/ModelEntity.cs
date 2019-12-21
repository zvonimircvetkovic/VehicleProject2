using System.ComponentModel.DataAnnotations;

namespace Project.DAL.Entities
{
    public class ModelEntity : IModelEntity
    {
        public int Id { get; set; }
        [Required]
        public int MakeId { get; set; }
        public virtual MakeEntity Make { get; set; }
        [Required]
        [Display(Name = "Model Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
    }
}
