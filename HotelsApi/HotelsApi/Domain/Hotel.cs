using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Domain
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "RoomsAvailable must be zero or postive integer.")]
        public int RoomsAvailable { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "RegionValue must be zero or postive integer.")]
        public int RegionValue { get; set; }
    }
}