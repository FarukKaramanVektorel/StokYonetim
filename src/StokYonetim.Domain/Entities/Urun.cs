using StokYonetim.Domain.Common;
using StokYonetim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public class Urun:BaseEntity
    {
        public string Ad { get; set; } = null!;
        public string? Barkod { get; set; }
        public Birim Birim { get; set; } = Birim.Adet;
        public decimal SatisFiyati { get; set; }
        public decimal VarsayilanKritikSeviye { get; set; }
        public bool Aktif { get; set; } = true;

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; } = null!;


        public int? TedarikciId { get; set; }
        public Tedarikci? Tedarikci { get; set; }




    }
}
