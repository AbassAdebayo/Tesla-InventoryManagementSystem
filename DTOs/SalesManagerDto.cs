using System.ComponentModel.DataAnnotations;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class SalesManagerDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public int Id { get; set; }
    }

    public class RegisterSalesManagerRequestModel
    {
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 2)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 8)]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.PostalCode)]
        public string Address { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
    }
    
    public class UpdateSalesManagerRequestModel
    {
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 2)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.PostalCode)]
        public string Address { get; set; }
        
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 8)]
        public string UserName { get; set; }
    }
}
