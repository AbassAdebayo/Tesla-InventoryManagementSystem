namespace InventoryManagemenSystem_Ims.Entities
{
    public class CheckOutSales: BaseEntity
    {
        public int ItemId { get; set; }
        
        public int Quantity { get; set; }
    }
}