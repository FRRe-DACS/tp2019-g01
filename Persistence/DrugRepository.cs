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
    public class DrugRepository : BaseRepository, IDrugRepository
    {
        public DrugRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Drug>> ListAsync(string name, string order)
        {
            var drugs = _context.Drugs.Where(d => name == null || d.Name.StartsWith(name));

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
                    drugs = drugs.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    drugs = drugs.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await drugs.ToListAsync();
        }

        public async Task<Drug> FindAsync(int id)
        {
            return await _context.Drugs.FindAsync(id);
        }

        public EntityState Update(Drug drug)
        {
            return _context.Entry(drug).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(Drug drug)
        {
            return await _context.Drugs.AddAsync(drug);
        }

        public EntityEntry Delete(Drug drug)
        {
            return _context.Drugs.Remove(drug);
        }

        public bool DrugExists(int id)
        {
            return _context.Drugs.Any(e => e.Id == id);
        }
    }
}