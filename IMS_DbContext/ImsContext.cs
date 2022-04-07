using InventoryManagemenSystem_Ims.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.IMS_DbContext
{
    public class ImsContext:DbContext
    
    {

        public ImsContext(DbContextOptions<ImsContext> options)
            : base(options)
        {
            
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<StockItem> StockItems { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<SalesItem> SalesItems { get; set; }
        
        public DbSet<SalesManager> SalesManagers { get; set; }
        
        public DbSet<Stock> Stocks { get; set; }
        
        public DbSet<Supplier> Suppliers { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<AllocateSalesItemToSalesManager> AllocateSalesItemToSalesManagers { get; set; } 
        
        public DbSet<Notification> Notifications { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<ShopManager> ShopManagers { get; set; }
        
        public DbSet<StockKeeper> StockKeepers { get; set; }
        
        public DbSet<Sales> Sales { get; set; }
        
        public DbSet<Item> Items { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<ItemCategory> ItemCategories { get; set; }
        
        public DbSet<Report> Reports { get; set; }
        
        public DbSet<CheckOutSales> CheckOutSales { get; set; }
        
        public DbSet<ReturnGoods> ReturnGoods { get; set; }
        
        
        
       
        
        

    }
    
    
}