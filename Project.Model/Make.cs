using Project.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Make : IMake
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
        [Required]
        [Display(Name = "Make Name")]
        public string Name { get; set; }
        [Display(Name = "Models")]
        public List<IModel> Models { get; set; }
    }
}
