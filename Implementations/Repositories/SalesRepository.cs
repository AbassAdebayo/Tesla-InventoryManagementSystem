﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ImsContext _imsContext;

        public SalesRepository(ImsContext imsContext)
        {
            _imsContext = imsContext;
        }

        public async Task<Sales> CreateSales(Sales sales)
        {
            await _imsContext.Sales.AddAsync(sales);
            await _imsContext.SaveChangesAsync();
            return sales;
        }

        public async Task<Sales> FindSalesById(int id)
        {
            return await _imsContext.Sales.SingleAsync(x=>x.Id==id);

        }

        public async Task<Sales> UpdateSales(int id, Sales sales)
        {
            _imsContext.Sales.Update(sales);
            await _imsContext.SaveChangesAsync();
            return sales;
        }

        public async Task<bool> DeleteSales(int id)
        {
            var sales = await _imsContext.Sales.FindAsync(id);
            _imsContext.Sales.Remove(sales);
            await _imsContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _imsContext.Sales.AnyAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sales>> GetAllSales()
        {
            return await _imsContext.Sales.Include(i => i.Item).Include(c => c.Customer).Include(s => s.SalesManager)
                .Select(sale => new Sales
                {
                    Customer = sale.Customer,
                    Id = sale.Id,
                    Description = sale.Description,
                    Item = sale.Item,
                    SalesManager = sale.SalesManager,
                    PricePerUnit = sale.PricePerUnit,
                    Quantity = sale.Quantity,
                    DateCreated = sale.DateCreated

                }).ToListAsync();
        }

        
        

        public async Task<bool> DeleteSalesItem(int id)
        {
            var salesItem = await _imsContext.SalesItems.FindAsync(id);
            _imsContext.SalesItems.Remove(salesItem);
            await _imsContext.SaveChangesAsync();
            return true;
        }
        
        

        public async Task<IList<SalesItem>> GetAllSalesItems()
        {
            return await _imsContext.SalesItems.ToListAsync();
        }


        public async Task<Sales> FindSalesByItemId(int itemId)
        {
            return await _imsContext.Sales.SingleOrDefaultAsync(x=>x.Id==itemId);
        }

        public async Task<decimal> GetGrandTotalOfAllSales()
        {
            return await _imsContext.Sales.SumAsync(s => s.TotalPrice);
        }

        // public Task<IEnumerable<SalesItem>> GetAllSalesItemBySalesManagerId(int id)
        // {
        //     throw new NotImplementedException();
        // }

        public async Task<IList<Sales>> GetSalesItemByDate(DateTime date)
        {
            return await _imsContext.Sales.Include(c => c.Customer).ThenInclude(c => c.Email).Include(c => c.Customer)
                .ThenInclude(c => c.FirstName).Include(c => c.Customer).ThenInclude(c => c.LastName)
                .Include(x => x.SalesItems)
                .Where(d => d.DateCreated == date).Select(s => new Sales()
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    SalesManagerId = s.SalesManagerId,
                    DateCreated = s.DateCreated,
                    Description = s.Description,
                    ItemId = s.ItemId,
                    PricePerUnit = s.PricePerUnit,
                    Quantity = s.Quantity,
                    TotalPrice = s.TotalPrice


                }).ToListAsync();

        }

        public async Task<List<Sales>> GenerateInvoice(int id)
        {
            var sales = await _imsContext.Sales.Include(c => c.Customer).Include(i => i.Item)
                .Include(s => s.SalesManager).Where(s => s.Id == id).Select(s => new Sales
                {
                    Customer = s.Customer,
                    SalesManager = s.SalesManager,
                    Description = s.Description,
                    Item = s.Item,
                    PricePerUnit = s.PricePerUnit,
                    Quantity = s.Quantity,
                    DateCreated = s.DateCreated,
                    Id = s.Id,
                    TotalPrice = s.TotalPrice

                }).ToListAsync();
            return sales;
        }

        public JsonResult ManageCustomersPatronage(int customerId)
        {
            var customerIdCount = _imsContext.Sales.Count(c => c.CustomerId == customerId);
            return new JsonResult(new {myCutomerIdCount = customerIdCount});
        }
        

        public IList<Sales> GetSalesByMonth(DateTime date)
        {
            
            return  _imsContext.Sales.Include(x=>x.Item).Include(x=>x.SalesManager).Include(x=>x.Customer).Where(x => x.DateCreated.Date== date.Date).Select(sales => new Sales
            {
                Customer = sales.Customer,
                Description = sales.Description,
                Id = sales.Id,
                Item = sales.Item,
                 SalesManager = sales.SalesManager,
                 Quantity = sales.Quantity,
                 PricePerUnit = sales.PricePerUnit,
                 TotalPrice = sales.TotalPrice,
                 DateCreated = sales.DateCreated
            }).ToList();


        }


    }
}