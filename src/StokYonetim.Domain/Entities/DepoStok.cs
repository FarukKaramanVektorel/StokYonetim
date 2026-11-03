using StokYonetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public  class DepoStok:BaseEntity
    {
        public int DepoId { get; set; }
        public Depo Depo { get; set; } = null!;
        public int UrunId { get; set; }
        public Urun Urun { get; set; } = null!;

        public decimal Miktar { get; set; }
        public decimal KritikSeviye { get; set; }
        public bool Kritikmi => Miktar <= KritikSeviye; 

        private DepoStok() { }



        public static DepoStok Olustur(int depoId, Urun urun) => new()
        {
            DepoId = depoId,
            UrunId = urun.Id,
            Miktar = 0,
            KritikSeviye = urun.VarsayilanKritikSeviye
        };

        public void KritikSeviyeDegistir(decimal deger)
        {
            if (deger < 0)
                throw new Exception("Kritik seviye negatif olamaz");

            KritikSeviye = deger;

        }


    }
}
