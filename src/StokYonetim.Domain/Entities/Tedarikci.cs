using StokYonetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public class Tedarikci:BaseEntity
    {
        public string Ad { get; set; } = null!;
        public string?  Telefon { get; set; } 
        public string? Eposta { get; set; }
        public bool Aktif { get; set; } = true;
    }
}
