using StokYonetim.Domain.Common;
using StokYonetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Tests.Domain
{
    public class KategoriTests
    {
        private static Kategori Yeni(int id,string ad,Kategori? ust = null)
        {
            var kategori = Kategori.Olustur(ad, ust);
            kategori.Id = id;
            kategori.YolHesapla();
            return kategori;
        }

        [Fact]
        public void Yol_ve_seviye_ust_kategoriden_turetilir()
        {
            var elektronik = Yeni(1, "Elektronik");
            var bilgisayar = Yeni(4, "Elektronik",elektronik);
            Assert.Equal("/1/4/", bilgisayar.Yol);
            Assert.Equal(2, bilgisayar.Seviye);
        }


        [Fact]
        public void Tasima_alt_agacin_yolunu_ve_seviyesini_gunceller()
        {
            var a = Yeni(1, "A");
            var b = Yeni(2, "B",a);
            var c = Yeni(3, "C",b);
            b.Tasi(yeniUst: null, altAgac: [c]);

            Assert.Equal("/2/", b.Yol);
            Assert.Equal(1, b.Seviye);
            Assert.Equal("/2/3/", c.Yol);
            Assert.Equal(2, c.Seviye);

        }


        [Fact]
        public void Kategori_kendi_altina_tasinamaz()
        {
            var a = Yeni(1, "A");
            var b = Yeni(2, "B", a);

            Assert.Throws<DomainException>(() => a.Tasi(b, [b]));
        }

        [Fact]
        public void Derinlik_bes_seviyeyi_asamaz()
        {
            var k1 = Yeni(1, "1");
            var k2 = Yeni(2, "2", k1);
            var k3 = Yeni(3, "3", k2);
            var k4 = Yeni(4, "4", k3);
            var k5 = Yeni(5, "5", k4);

            Assert.Throws<DomainException>(() => Kategori.Olustur("6", k5));
        }
    }
}
