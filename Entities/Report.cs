using InventoryManagemenSystem_Ims.Enums;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Report: BaseEntity
    {
        public string Description { get; set; }
        
        public ReportStatus ReportStatus { get; set; }
        
        public string SalesManagerReport { get; set; }
        
        public string StockKeeperReport{get; set; }
        
        public string Content { get; set; }
        
        public string Subject { get; set; }
        
        
    }
}