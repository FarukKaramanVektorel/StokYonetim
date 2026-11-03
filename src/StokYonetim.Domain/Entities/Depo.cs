using StokYonetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public class Depo:BaseEntity
    {
        public string Ad { get; set; } = null!;
        public bool Aktif { get; set; } = true;
    }
}
