using System.ComponentModel.DataAnnotations;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Enums;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class ReportDto: BaseEntity
    {
        public string Description { get; set; }
        
        public int SalesManagerId { get; set; }
        
        public SalesManager SalesManager { get; set; }
        
        public string SalesManagerName { get; set; }
        
        public int StockKeeperId { get; set; }
        
        public StockKeeper StockKeeper { get; set; }
        
        
        public string Content { get; set; }
        
        public string Subject { get; set; }
        
        public ReportStatus ReportStatus { get; set; }
        
        public string SalesManagerReport { get; set; }
        
        public string StockKeeperReport{get; set; }
    }

    public class CreateSalesManagerReportModel
    {
        [Required(ErrorMessage = "Report field is required")]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Report field is required")]
        [StringLength(maximumLength: 1500)]
        public string SalesManagerReport { get; set; }

        public string Content { get; set; }
        
        public string Subject { get; set; }
        
        
      
    }
    
    public class CreateStockKeeperReportModel
    {
        [Required(ErrorMessage = "Report field is required")]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Report field is required")]
        [StringLength(maximumLength: 1500)]
        public string StockKeeperReport { get; set; }
        
        public string Content { get; set; }
        
        public string Subject { get; set; }
        
        
       
        
    }
}