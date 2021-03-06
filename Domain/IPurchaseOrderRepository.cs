﻿using GestionDeMedicamentos.Controllers;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IPurchaseOrderRepository
    {
        Task<PaginatedList<PurchaseOrder>> ListAsync(string date, string order, int? pageNumber, int? pageSize);
        Task<PurchaseOrder> FindAsync(int id);
        Task<EntityEntry> CreateAsync(PurchaseOrder purchaseOrder);
        EntityEntry Delete(PurchaseOrder purchaseOrder);
        Task SaveChangesAsync();
        bool PurchaseOrderExists(int id);
    }
}
