using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyDb.UnitTest.Models
{
    public class Money
    {
        public Money() { }

        public Money(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        [Required, StringLength(3)]
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
