using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Enums;

namespace InventoryManagemenSystem_Ims.DTOs
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        
        public bool Status { get; set; }
        
        public ReportStatus ReportStatus { get; set; }
        
        public T Data { get; set; }
    }
}