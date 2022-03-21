using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 8)]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IList<RoleDto> Roles { get; set; } = new List<RoleDto>();
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    
    public class LoginResponseDto : UserDto
    {
        public string Token { get; set; }
    }

    public class LoginDto
    {

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public IList<int> Roles { get; set; } = new List<int>();
       
    }

    public class UpdateUserRequestModel
    {
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 8)]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}