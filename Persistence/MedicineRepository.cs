﻿using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public class MedicineRepository : BaseRepository, IMedicineRepository
    {
        public MedicineRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Medicine>> ListAsync(string name, string drug, decimal? proportion, string presentation, string laboratory, string order)
        {
            var medicines = _context.Medicines.Include(m => m.Drug).Where(m => (name == null || m.Name.StartsWith(name)) && (drug == null || m.Drug.Name.StartsWith(drug)) && (proportion == null || m.Proportion == proportion) && (presentation == null || m.Presentation.ToString().StartsWith(presentation)) && (laboratory == null || m.Laboratory.StartsWith(laboratory)));

            bool descending = false;
            if (order != null)
            {
                order = order.Substring(0, 1).ToUpper() + order.Substring(1, order.Length - 1);
                if (order.EndsWith("_desc"))
                {
                    order = order.Substring(0, order.Length - 5);
                    descending = true;
                }

                if (descending)
                {
                    medicines = medicines.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    medicines = medicines.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await medicines.ToListAsync();
        }

        public async Task<Medicine> FindAsync(int id)
        {
            return await _context.Medicines.Include(m => m.Drug).FirstOrDefaultAsync(m => m.Id == id);
        }

        public EntityState Update(Medicine medicine)
        {
            return _context.Entry(medicine).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(Medicine medicine)
        {
            return await _context.Medicines.AddAsync(medicine);
        }

        public EntityEntry Delete(Medicine medicine)
        {
            return _context.Medicines.Remove(medicine);
        }

        public bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
    }
}