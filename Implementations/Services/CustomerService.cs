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
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<BaseResponse<bool>> CreateCustomer(CreateCustomerRequestModel model)
        {
            try
            {
                var customer = await _customerRepository.CustomerExistByCompanyNameAsync(model.CompanyName);
                if (customer != null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Customer already exist!",
                        Status = false

                    };
                }

                customer = new Customer()
                {
                    Address = model.Address,
                    Email = model.Email,
                    ShopName = model.CompanyName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    DateCreated = DateTime.UtcNow
                };
                await _customerRepository.CreateCustomerAsync(customer);
                return new BaseResponse<bool>
                {
                    Message = "Customer successfully created",
                    Status = true,

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<bool>> UpdateCustomer(int id, UpdateCustomerRequestModel model)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);

                if (customer==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Supplier with id {id} does not exist",
                        Status = false
                    };
                }

                customer.Address = model.Address;
                customer.Email = model.Email;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.ShopName = model.CompanyName;
                customer.PhoneNumber = model.PhoneNumber;
                customer.DateModified = DateTime.UtcNow;
                await _customerRepository.UpdateCustomerAsync(id, customer);
                return new BaseResponse<bool>
                {
                    Message = "Customer Successfully updated",
                    Status = true,

                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<bool>> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer==null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"The Customer with id {id} does not exist",
                        Status = false
                    };
                }

                await _customerRepository.DeleteCustomerAsync(customer);
                return new BaseResponse<bool>
                {
                    Message = "Customer Successfully deleted",
                    Status = true,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<CustomerDto>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return new BaseResponse<CustomerDto>
                    {
                        Message = $"The Customer with id {id} does not exist",
                        Status = false
                    };
                }

                return new BaseResponse<CustomerDto>
                {

                    Message = $"The Customer with id {id} retrieved",
                    Status = true,
                    Data = new CustomerDto()
                    {

                        Address = customer.Address,
                        Email = customer.Email,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        CompanyName = customer.ShopName,
                        Id = customer.Id
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<IList<CustomerDto>>> GetAllCustomers()
        {
            try
            {
                var customer = await _customerRepository.GetAllCustomers();
                return new BaseResponse<IList<CustomerDto>>
                {
                    Message = "Customers retrieved",
                    Status = true,
                    Data = customer.Select(s=>new CustomerDto()
                    {
                        Address = s.Address,
                        Email = s.Email,
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        PhoneNumber = s.PhoneNumber,
                        CompanyName = s.ShopName
                    
                    }).ToList()
                
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BaseResponse<CustomerDto>> CustomerExistsByShopName(string shopName)
        {
            try
            {
                var customer = await _customerRepository.CustomerExistByCompanyNameAsync(shopName);

                if (customer == null)
                {
                    return new BaseResponse<CustomerDto>
                    {
                        Message = "Customer cannot be found!",
                        Status = false,
                    };
                }

                return new BaseResponse<CustomerDto>
                {
                    Message = "Customer fetched",
                    Status = true,
                    Data = new CustomerDto()
                    {
                        Address = customer.Address,
                        Email = customer.Email,
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        CompanyName = customer.ShopName
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