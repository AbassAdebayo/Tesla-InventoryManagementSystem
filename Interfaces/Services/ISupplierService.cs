using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface ISupplierService
    {
        public Task<BaseResponse<bool>> CreateSupplier(CreateSupplierRequestModel model);
        
        public Task<BaseResponse<bool>> UpdateSupplier(int id, UpdateSupplierRequestModel model);
        
        public Task<BaseResponse<bool>> DeleteSupplier(int id);
        
        public Task<BaseResponse<SupplierDto>> GetSupplierById(int id);
        
        public Task<BaseResponse<IList<SupplierDto>>> GetAllSuppliers();

        public Task<BaseResponse<SupplierDto>> SupplierExistsByCompanyName(string companyName);
    }
}