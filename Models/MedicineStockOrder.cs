﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class MedicineStockOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        [Required]
        public int StockOrderId { get; set; }
        [ForeignKey("StockOrderId")]
        public StockOrder StockOrder { get; set; }
    }
}
