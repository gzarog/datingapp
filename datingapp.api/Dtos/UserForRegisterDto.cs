using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace datingapp.api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [StringLength(8,MinimumLength =4,ErrorMessage ="pass 4-8 chars")]
        public string Password { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        [Required]
        public string KnownAs { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string Country { get; set; }
        
       
        public DateTime Created { get; set; }
        
       
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            this.Created = DateTime.Now;
            this.LastActive = DateTime.Now;
        }


    }
}
