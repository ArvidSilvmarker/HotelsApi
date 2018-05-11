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
        [Range(1, int.MaxValue, ErrorMessage = "Value must be postive non-zero integer.")]
        public int Value { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    }
}
