using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class StockKeeperDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        
        public int Id { get; set; }
    }

    public class RegisterStockKeeperRequestModel
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
        
        public IList<int> Roles { get; set; } = new List<int>();
        
    }
    
    public class UpdateStockKeeperRequestModel
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