using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Api.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}