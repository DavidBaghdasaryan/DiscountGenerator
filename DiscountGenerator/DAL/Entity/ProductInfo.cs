﻿using System.ComponentModel.DataAnnotations;

namespace DiscountGenerator.DAL.Entity
{
    public class ProductInfo
    {
        [Key]
        public int ProductID  { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
