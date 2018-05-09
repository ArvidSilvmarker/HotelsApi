using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be non-zero.")]
        public int Value { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
