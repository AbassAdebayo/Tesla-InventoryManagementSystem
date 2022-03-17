﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.DTOs;
using InventoryManagemenSystem_Ims.Entities;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagemenSystem_Ims.Implementations.Repositories
{
    public class SalesRepository:ISalesRepository
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
            return await _imsContext.Sales.FindAsync(id);
            
        }

        public async Task<Sales> UpdateSales(int id, Sales sales)
        {
            _imsContext.Sales.Update(sales);
            await _imsContext.SaveChangesAsync();
            return sales;
        }

        public async Task<bool> DeleteSales(int id)
        {
            var sales= await _imsContext.Sales.FindAsync(id);
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
            return await _imsContext.Sales.ToListAsync();
        }

        public async Task<SalesItem> CreateSalesItem(SalesItem salesItem)
        {
            await _imsContext.SalesItems.AddAsync(salesItem);
            await _imsContext.SaveChangesAsync();
            return salesItem;
        }

        public async Task<SalesItem> UpdateSalesItem(int id, SalesItem salesItem)
        {
            _imsContext.SalesItems.Update(salesItem);
            await _imsContext.SaveChangesAsync();
            return salesItem;
        }

        public async Task<SalesItem> FindSalesItemById(int id)
        {
            return await _imsContext.SalesItems.FindAsync(id);
        }

        public async Task<bool> DeleteSalesItem(int id)
        {
            var salesItem = await _imsContext.SalesItems.FindAsync(id);
            _imsContext.SalesItems.Remove(salesItem);
            await _imsContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSalesItems()
        {
            var salesItems = await _imsContext.SalesItems.ToListAsync();
            foreach (var saleItem in salesItems)
            {
                _imsContext.SalesItems.Remove(saleItem);
                await _imsContext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<IList<SalesItem>> GetAllSalesItems(IEnumerable<int> salesItemIds)
        {
            return await _imsContext.SalesItems.Where(salesItem => salesItemIds.Contains(salesItem.Id)).ToListAsync();
        }

        public async Task<IList<SalesItem>> GetAllSalesItems()
        {
            return await _imsContext.SalesItems.ToListAsync();
        }


        public async Task<SalesItem> GetSalesItemById(int id)
        {
            return await _imsContext.SalesItems.FindAsync(id);
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
                .Where(d => d.DateCreated == date).Select(s => new Sales()
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    SalesManagerId = s.SalesManagerId,
                    DateCreated = s.DateCreated,
                    Description = s.Description,
                    TotalPrice = s.TotalPrice

                }).ToListAsync();
            
        }
    }
}