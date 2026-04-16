using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class Account : DomainObject
    {
        public User AccountHolder { get; set; }
        public double Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; } = [];
        public string NoSirveParanada { get; set; } = "Esto es un ejemplo de propiedad que no sirve para nada";

    }
}
