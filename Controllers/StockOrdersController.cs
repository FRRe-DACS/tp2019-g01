﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestiónDeMedicamentos.Models;
using GestiónDeMedicamentos.Domain;
using System;

namespace GestiónDeMedicamentos.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockOrdersController : ControllerBase
    {
        private readonly IStockOrderRepository _stockOrderRepository;
        private readonly IMedicineRepository _medicineRepository;

        public StockOrdersController(IStockOrderRepository stockOrderRepository, IMedicineRepository medicineRepository)
        {
            _stockOrderRepository = stockOrderRepository;
            _medicineRepository = medicineRepository;
        }

        //GET: api/stock/?date=01/01/2019
        [HttpGet]
        public async Task<IActionResult> GetStockOrders(string date, string order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<StockOrder> stockOrders = await _stockOrderRepository.ListAsync(date, order);

            return Ok(stockOrders);
        }


        // GET: api/stock/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockOrder = await _stockOrderRepository.FindAsync(id);

            if (stockOrder == null)
            {
                return NotFound();
            }

            return Ok(stockOrder);
        }

        // POST: api/StockOrder
        [HttpPost]
        public async Task<IActionResult> PostStockOrder([FromBody] StockOrder stockOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _stockOrderRepository.CreateAsync(stockOrder);

            foreach (var medicineStockOrder in stockOrder.MedicineStockOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicineStockOrder.MedicineId);
                if (medicineStockOrder.Quantity < 0)
                {
                    medicine.Stock -= (uint)Math.Abs(medicineStockOrder.Quantity);
                }
                else
                {
                    medicine.Stock += (uint)medicineStockOrder.Quantity;
                }
                _medicineRepository.Update(medicine);
            }

            await _stockOrderRepository.SaveChangesAsync();

            return CreatedAtAction("GetStockOrder", new { id = stockOrder.Id }, stockOrder);
        }

        // DELETE: api/stock/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockOrder = await _stockOrderRepository.FindAsync(id);
            if (stockOrder == null)
            {
                return NotFound();
            }

            foreach (var medicineStockOrder in stockOrder.MedicineStockOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicineStockOrder.MedicineId);
                if (medicineStockOrder.Quantity < 0)
                {
                    medicine.Stock += (uint)Math.Abs(medicineStockOrder.Quantity);
                }
                else
                {
                    medicine.Stock -= (uint)medicineStockOrder.Quantity;
                }
                _medicineRepository.Update(medicine);
            }

            _stockOrderRepository.Delete(stockOrder);
            await _stockOrderRepository.SaveChangesAsync();

            return Ok(stockOrder);
        }


    }
}