using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IAllocateSalesItemToSalesManagerRepository
    {
        Task<AllocateSalesItemToSalesManager> AllocateSalesItem(
            AllocateSalesItemToSalesManager allocateSalesItemToSalesManager);

        Task<bool> DeleteAllocatedSalesItem(AllocateSalesItemToSalesManager allocateSalesItemToSalesManager);

        Task<IList<AllocateSalesItemToSalesManager>> GetAllAllocatedSalesItem();

        Task<AllocateSalesItemToSalesManager> GetAllocatedSalesItem(int id);

        Task<AllocateSalesItemToSalesManager> UpdateAllocatedSalesItem(int id,
            AllocateSalesItemToSalesManager allocateSalesItemToSalesManager);

        Task<AllocateSalesItemToSalesManager> GetAllocatedItemsByItemId(int itemId);

    }
}