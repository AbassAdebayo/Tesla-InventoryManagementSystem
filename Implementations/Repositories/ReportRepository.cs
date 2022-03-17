using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Enums;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class ReportRepository:IReportRepository
    {
        private readonly ImsContext _imsContext;

        public ReportRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }

        public async Task<Report> CreateReport(Report report)
        {
            await _imsContext.Reports.AddAsync(report);
            await _imsContext.SaveChangesAsync();
            return report;
        }

        public async Task<Report> UpdateReport(int id, Report report)
        {
            _imsContext.Reports.Update(report);
            await _imsContext.SaveChangesAsync();
            return report;
        }

        public async Task<Report> DeleteReport(Report report)
        {
            _imsContext.Reports.Remove(report);
            await _imsContext.SaveChangesAsync();
            return report;
        }

        public async Task<IList<Report>> GetAllReports()
        {
            return await _imsContext.Reports.ToListAsync();
        }

        public async Task<Report> GetReport(int id)
        {
           return await _imsContext.Reports.FindAsync(id);
        }

        public async Task<IEnumerable<Report>> ViewStockKeeperVerifiedReports()
        {
            return await _imsContext.Reports.Where(r => r.ReportStatus == ReportStatus.Verified).Select(report =>
                new Report()
                {
                    Description = report.Description,
                    StockKeeperReport = report.StockKeeperReport,
                    Id = report.Id,
                    DateCreated = report.DateCreated,
                    DateModified = report.DateModified,
                    
                    

                }).ToListAsync();
        }

        public async Task<IEnumerable<Report>> ViewSalesManagerVerifiedReports()
        {
            return await _imsContext.Reports.Where(r => r.ReportStatus == ReportStatus.Verified).Select(report =>
                new Report()
                {
                    Description = report.Description,
                    SalesManagerReport = report.SalesManagerReport,
                    Id = report.Id,
                    DateCreated = report.DateCreated,
                    DateModified = report.DateModified,
                    
                    

                }).ToListAsync();
        }

        public async Task<IEnumerable<Report>> ViewStockKeeperApprovedReports()
        {
            return await _imsContext.Reports.Where(r => r.ReportStatus == ReportStatus.Approved).Select(report =>
                new Report()
                {
                    Description = report.Description,
                    StockKeeperReport = report.StockKeeperReport,
                    Id = report.Id,
                    DateCreated = report.DateCreated,
                    DateModified = report.DateModified,
                    
                    

                }).ToListAsync();
        }

        public async Task<IEnumerable<Report>> ViewSalesManagerApprovedReports()
        {
            return await _imsContext.Reports.Where(r => r.ReportStatus == ReportStatus.Approved).Select(report =>
                new Report()
                {
                    Description = report.Description,
                    SalesManagerReport = report.SalesManagerReport,
                    Id = report.Id,
                    DateCreated = report.DateCreated,
                    DateModified = report.DateModified,
                    
                    

                }).ToListAsync();
        }
    }
    
}