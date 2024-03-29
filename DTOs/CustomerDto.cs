﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class CustomerDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string CompanyName { get; set; }
        
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
    }

    public class CreateCustomerRequestModel
    {
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(maximumLength:25, MinimumLength = 2)]
        public string LastName { get; set; }
        
        
        [Required]
        [StringLength(maximumLength:50, MinimumLength = 8)]
        public string CompanyName { get; set; }
        
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
    
    public class UpdateCustomerRequestModel
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
        [StringLength(maximumLength:50, MinimumLength = 8)]
        public string CompanyName { get; set; }
    }
}