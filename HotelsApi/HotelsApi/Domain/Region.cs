using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain
{
    public class Region
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
