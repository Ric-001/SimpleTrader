using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class Asset
    {
        public string Symbol { get; set; } = string.Empty;
        public double PricePerShare { get; set; }

    }
}
