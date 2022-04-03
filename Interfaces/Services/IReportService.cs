using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IReportService
    {
        Task<BaseResponse<ReportDto>> SubmitSalesManagerReport(CreateSalesManagerReportModel report, int id);

        Task<BaseResponse<ReportDto>> SubmitStockKeeperReport(CreateStockKeeperReportModel report, string userName);
        

        Task<BaseResponse<ReportDto>> DeleteReport(int id);

        Task<IList<ReportDto>> GetAllStockKeeperReports();
         
        Task<IList<ReportDto>> GetAllSalesManagerReports();

        Task<BaseResponse<bool>> UpdateReportStatusToVerified(int id);
        
        Task<BaseResponse<bool>> UpdateReportStatusToApproved(int id);
        
        Task<IEnumerable<ReportDto>> ViewStockKeeperVerifiedReports();
        
        Task<IEnumerable<ReportDto>> ViewSalesManagerVerifiedReports();

        Task<IEnumerable<ReportDto>> ViewStockKeeperApprovedReports();
        
        Task<IEnumerable<ReportDto>> ViewSalesManagerApprovedReports();
        
    }
}