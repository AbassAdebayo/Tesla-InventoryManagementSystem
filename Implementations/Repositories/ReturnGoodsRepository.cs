using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class ReturnGoodsRepository:IReturnGoodsRepository
    {
        private readonly ImsContext _imsContext;

        public ReturnGoodsRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }
        public async Task<ReturnGoods> ReturnGoods(ReturnGoods returnGoods)
        {
           await _imsContext.ReturnGoods.AddAsync(returnGoods);
           await _imsContext.SaveChangesAsync();
           return returnGoods;
        }
    }
}