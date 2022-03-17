namespace InventoryManagemenSystem_Ims.Entities
{
    public class ShopManager: BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
        
        public int UserId { get; set; }
        
        public User User { get; set; }
    }
}