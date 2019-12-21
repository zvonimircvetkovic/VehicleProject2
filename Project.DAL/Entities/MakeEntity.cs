using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.DAL.Entities
{
    public class MakeEntity : IMakeEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Make Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
        [Display(Name = "Models")]
        public virtual IEnumerable<ModelEntity> ModelEntities { get; set; }
    }
}
