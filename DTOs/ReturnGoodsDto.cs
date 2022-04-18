using System;
using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class ReturnGoodsDto
    {
        public int Id { get; set; }
        
        public int SalesId { get; set; }
        
        public Sales Sales { get; set; }
        
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public string ReturnType { get; set; }
        
        public string Description { get; set; }
        
        public string QuantityReturned { get; set; }
    }

    public class ReturnGoodsRequestModel
    {
        //public int Id { get; set; }
        
        public int SalesId { get; set; }
        
        public Sales Sales { get; set; }
        
        public int CustomerId { get; set; }
        
        public int QuantityReturned { get; set; }
        
        public string ReturnType { get; set; }
        
        public string Description { get; set; }
        
        public int SalesManagerId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        
        
    }
}