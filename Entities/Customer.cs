using System.Collections.Generic;

namespace InventoryManagemenSystem_Ims.Entities
{
    public class Customer: BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string ShopName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }

        public IList<ReturnGoods> ReturnGoods { get; set; } = new List<ReturnGoods>();




    }
}