namespace InventoryManagemenSystem_Ims.Entities
{
    public class Supplier:BaseEntity
    {

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string CompanyName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
        
    }
}