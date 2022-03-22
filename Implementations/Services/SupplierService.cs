using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;

namespace InventoryManagemenSystem_Ims.Implementations.Services
{
    public class SupplierService: ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<BaseResponse<bool>> CreateSupplier(CreateSupplierRequestModel model)
        {
            try
            {
                var supplier = await _supplierRepository.SupplierExistByCompanyNameAsync(model.CompanyName);
                if (supplier!=null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Supplier already exist!",
                        Status = false

                    };
                }

                var newSupplier = new Supplier
                {
                    Address = model.Address,
                    Email = model.Email,
                    CompanyName = model.CompanyName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    DateCreated = DateTime.UtcNow
                };
                await _supplierRepository.AddSupplierAsync(newSupplier);
                return new BaseResponse<bool>
                {
                    Message = "Supplier successfully created",
                    Status = true,

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<bool>> UpdateSupplier(int id, UpdateSupplierRequestModel model)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(id);

                if (supplier==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Supplier with id {id} does not exist",
                        Status = false
                    };
                }

                supplier.Address = model.Address;
                supplier.Email = model.Email;
                supplier.FirstName = model.FirstName;
                supplier.LastName = model.LastName;
                supplier.CompanyName = model.CompanyName;
                supplier.PhoneNumber = model.PhoneNumber;
                supplier.DateModified=DateTime.UtcNow;
                await _supplierRepository.UpdateSupplierAsync(id, supplier);
                return new BaseResponse<bool>
                {
                    Message = "supplier Successfully updated",
                    Status = true,

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<bool>> DeleteSupplier(int id)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
                if (supplier==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Supplier with id {id} does not exist",
                        Status = false
                    };
                }

                await _supplierRepository.DeleteSupplierAsync(supplier);
                return new BaseResponse<bool>
                {
                    Message = "User Successfully deleted",
                    Status = true,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SupplierDto> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
                if (supplier == null)
                {
                    throw new Exception("Supplier not found!");
                }

                return new SupplierDto
                {

                    Address = supplier.Address,
                    Email = supplier.Email,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    PhoneNumber = supplier.PhoneNumber,
                    CompanyName = supplier.CompanyName,
                    Id = supplier.Id,
                    DateCreated = supplier.DateCreated
                    
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliers()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllSuppliers();

                return suppliers.Select(s => new SupplierDto()
                {
                    Address = s.Address,
                    Email = s.Email,
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNumber = s.PhoneNumber,
                    CompanyName = s.CompanyName,
                    DateCreated = s.DateCreated

                }).ToList();

            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<SupplierDto>> SupplierExistsByCompanyName(string companyName)
        {
            try
            {
                var supplier = await _supplierRepository.SupplierExistByCompanyNameAsync(companyName);

                if (supplier==null)
                {
                    return new BaseResponse<SupplierDto>
                    {
                        Message = "Supplier cannot be found!",
                        Status = false,
                    };
                }

                return new BaseResponse<SupplierDto>
                {
                    Message = "Supplier fetched",
                    Status = true,
                    Data = new SupplierDto
                    {
                        Address = supplier.Address,
                        Email = supplier.Email,
                        Id = supplier.Id,
                        FirstName = supplier.FirstName,
                        LastName = supplier.LastName,
                        PhoneNumber = supplier.PhoneNumber,
                        CompanyName = supplier.CompanyName
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}