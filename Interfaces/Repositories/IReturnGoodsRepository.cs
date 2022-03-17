using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface  IReturnGoodsRepository
    {
        public Task<ReturnGoods> ReturnGoods(ReturnGoods returnGoods);
        
    }
}