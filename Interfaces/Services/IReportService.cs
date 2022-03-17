using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;

namespace InventoryManagemenSystem_Ims.Interfaces.Services
{
    public interface IReportService
    {
        Task<BaseResponse<ReportDto>> SubmitSalesManagerReport(CreateSalesManagerReportModel report, string userName);

        Task<BaseResponse<ReportDto>> SubmitStockKeeperReport(CreateStockKeeperReportModel report);
        
        Task<BaseResponse<ReportDto>> DeleteSalesManagerReport(int id);

        Task<BaseResponse<ReportDto>> DeleteStockKeeperReport(int id);

        Task<BaseResponse<IList<ReportDto>>> GetAllStockKeeperReports();
         
        Task<BaseResponse<IList<ReportDto>>> GetAllSalesManagerReports();

        Task<BaseResponse<bool>> UpdateReportStatusToVerified(int id);
        
        Task<BaseResponse<bool>> UpdateReportStatusToApproved(int id);
        
        Task<BaseResponse<IEnumerable<ReportDto>>> ViewStockKeeperVerifiedReports();
        
        Task<BaseResponse<IEnumerable<ReportDto>>> ViewSalesManagerVerifiedReports();

        Task<BaseResponse<IEnumerable<ReportDto>>> ViewStockKeeperApprovedReports();
        
        Task<BaseResponse<IEnumerable<ReportDto>>> ViewSalesManagerApprovedReports();
        
    }
}