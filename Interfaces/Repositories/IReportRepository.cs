using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<Report> CreateReport(Report report);
        Task<Report> UpdateReport(int id, Report report);
        
        Task<Report> DeleteReport(Report report);
        
        Task<IList<Report>> GetAllReports();
         
        Task<Report> GetReport(int id);

        Task<IEnumerable<Report>> ViewStockKeeperVerifiedReports();
        
        Task<IEnumerable<Report>> ViewSalesManagerVerifiedReports();

        Task<IEnumerable<Report>> ViewStockKeeperApprovedReports();
        
        Task<IEnumerable<Report>> ViewSalesManagerApprovedReports();
    }
}