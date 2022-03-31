using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Enums;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.SendMail;
using MailKit;


namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class ReportService:IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ISalesManagerRepository _salesManagerRepository;
        private readonly IStockKeeperRepository _stockKeeperRepository;
        private readonly IMailMessage _mailMessage;
        private readonly IUserService _userService;

        public ReportService(IReportRepository reportRepository, ISalesManagerRepository 
            salesManagerRepository, IStockKeeperRepository stockKeeperRepository, IMailMessage mailMessage, IUserService userService)
        {
            _reportRepository = reportRepository;
            _salesManagerRepository = salesManagerRepository;
            _stockKeeperRepository = stockKeeperRepository;
            _mailMessage = mailMessage;
            _userService = userService;
        }

        public async Task<BaseResponse<ReportDto>> SubmitSalesManagerReport(CreateSalesManagerReportModel report, int id)
        {
            
            try
            {
                var getById = await _salesManagerRepository.GetSalesManagerByIdAsync(id); 
                var newReport = new Report
                {
                    Subject = report.Subject,
                    SalesManagerReport = report.SalesManagerReport,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,

                };
                //_mailMessage.SendEmailAddressFromSalesManager(getById.Email, report.Subject, report.SalesManagerReport);
                await _reportRepository.CreateReport(newReport);
                return new BaseResponse<ReportDto>
                {
                    Message = "Report sent",
                    ReportStatus = ReportStatus.Sent,
                    Data = new ReportDto
                    {
                        Subject = newReport.Subject,
                        Description = newReport.Description,
                        SalesManagerReport = report.SalesManagerReport,
                        DateCreated = DateTime.UtcNow,
                       
                    }
                };

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<ReportDto>> SubmitStockKeeperReport(CreateStockKeeperReportModel report)
        {
            try
            {
                //var getUserName = await _stockKeeperRepository.GetStockKeeperByUsernameAsync(stockKeeperUserName); 
                var newReport = new Report
                {
                    Subject = report.Subject,
                    Description = report.Description,
                    StockKeeperReport = report.StockKeeperReport,
                    DateCreated = DateTime.UtcNow,
                   

                };
               // _mailMessage.SendEmailAddressFromStockKeeper(getUserName.Email, newReport.Content, newReport.Subject);
                await _reportRepository.CreateReport(newReport);
                return new BaseResponse<ReportDto>
                {
                    Message = "Report sent",
                    ReportStatus = ReportStatus.Sent,
                    Data = new ReportDto
                    {
                        Description = newReport.Description,
                        StockKeeperReport = report.StockKeeperReport,
                        DateCreated = DateTime.UtcNow,
                      
                    }
                };

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<ReportDto>> DeleteSalesManagerReport(int id)
        {
            try
            {
                var report = await _reportRepository.GetReport(id);
                await _reportRepository.DeleteReport(report);
            
                return new BaseResponse<ReportDto>
                {
                    Message = "Data deleted!",
                    Status = true
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<BaseResponse<ReportDto>> DeleteStockKeeperReport(int id)
        {
            try
            {
                var report = await _reportRepository.GetReport(id);
                await _reportRepository.DeleteReport(report);
            
                return new BaseResponse<ReportDto>
                {
                    Message = "Data deleted!",
                    Status = true
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IList<ReportDto>> GetAllStockKeeperReports()
        {
            var stockKeepersReports = await _reportRepository.GetAllReports();



            return stockKeepersReports.Select(s => new ReportDto()
            {
                Description = s.Description,
                Id = s.Id,
                StockKeeperReport = s.StockKeeperReport,
                DateCreated = s.DateCreated,
                DateModified = s.DateModified

            }).ToList();

        }

        public async Task<IList<ReportDto>> GetAllSalesManagerReports()
        {
            try
            {
                var salesManagerReport = await _reportRepository.GetAllReports();
                
                return salesManagerReport.Select(s => new ReportDto
                {
                    Subject = s.Subject,
                    Description = s.Description,
                    Id = s.Id,
                    SalesManagerReport = s.SalesManagerReport,
                    DateCreated = s.DateCreated,
                    DateModified = s.DateModified,


                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<bool>> UpdateReportStatusToVerified(int id)
        {
            try
            {
                var report = await _reportRepository.GetReport(id);
                report.ReportStatus = ReportStatus.Verified;
                await _reportRepository.UpdateReport(id, report);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return new BaseResponse<bool>
            {
                Message = "Report verified",
                Status = true,
                ReportStatus = ReportStatus.Verified
            };
        }

        public async Task<BaseResponse<bool>> UpdateReportStatusToApproved(int id)
        {
            try
            {
                var report = await _reportRepository.GetReport(id);
                report.ReportStatus = ReportStatus.Approved;
                await _reportRepository.UpdateReport(id, report);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return new BaseResponse<bool>
            {
                Message = "Report approved",
                Status = true,
                ReportStatus = ReportStatus.Approved
            };
        }

        public async Task<IEnumerable<ReportDto>> ViewStockKeeperVerifiedReports()
        {
            var reports= await _reportRepository.ViewStockKeeperVerifiedReports();



            return reports.Select(report => new ReportDto()
            {
                Content = report.Content,
                Description = report.Description,
                Id = report.Id,
                ReportStatus = ReportStatus.Verified,
                DateCreated = report.DateCreated,
                DateModified = report.DateModified

            }).ToList();

        }

        public async Task<IEnumerable<ReportDto>> ViewSalesManagerVerifiedReports()
        {
            var reports= await _reportRepository.ViewSalesManagerVerifiedReports();




            return reports.Select(report => new ReportDto()
            {
                Content = report.Content,
                Description = report.Description,
                Id = report.Id,
                ReportStatus = ReportStatus.Verified,
                DateCreated = report.DateCreated,
                DateModified = report.DateModified

            }).ToList();

        }

        public async Task<IEnumerable<ReportDto>> ViewStockKeeperApprovedReports()
        {
            var reports= await _reportRepository.ViewStockKeeperApprovedReports();



            return reports.Select(report => new ReportDto()
            {
                Content = report.Content,
                Description = report.Description,
                Id = report.Id,
                ReportStatus = ReportStatus.Approved,
                DateCreated = report.DateCreated,
                DateModified = report.DateModified

            }).ToList();

        }

        public async Task<IEnumerable<ReportDto>> ViewSalesManagerApprovedReports()
        {
            var reports= await _reportRepository.ViewSalesManagerApprovedReports();




            return reports.Select(report => new ReportDto()
            {
                Content = report.Content,
                Description = report.Description,
                Id = report.Id,
                ReportStatus = ReportStatus.Approved,
                DateCreated = report.DateCreated,
                DateModified = report.DateModified

            }).ToList();

        }
    }
}