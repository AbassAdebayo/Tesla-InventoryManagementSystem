using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IAllocateSalesItemToSalesManagerService
    {
        Task<AllocateSalesItemToSalesManagerDto> AllocateSalesItem(
            CreateAllocationResponseModel allocateSalesItemToSalesManager);

        Task<bool> DeleteAllocatedSalesItem(AllocateSalesItemToSalesManager allocateSalesItemToSalesManager);

        Task<IList<AllocateSalesItemToSalesManager>> GetAllAllocatedSalesItem();

        Task<AllocateSalesItemToSalesManager> GetAllocatedSalesItem(int id);
    }
}