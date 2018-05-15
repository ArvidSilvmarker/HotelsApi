using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelsApi.Domain;

namespace HotelsApi.Common
{
    public class HotelValidation
    {

        public static bool ValidateHotel(Hotel hotel)
        {
            var context = new ValidationContext(hotel);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(hotel, context, results);
        }
        

    }
}
