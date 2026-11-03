using StokYonetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public class Kategori : BaseEntity
    {
        public const int MaksimumSeviye = 5;
        public string Ad { get; private set; } = null!;

        public int? UstKategoriId { get; private set; }
        public Kategori UstKategori { get; private set; }
        public string Yol { get; private set; } = null!;
        public int Seviye { get; private set; }

        private readonly List<Kategori> _altKategoriler = [];
        public IReadOnlyCollection<Kategori> AltKategoriler => _altKategoriler;

        private Kategori() { }

        public static Kategori Olustur(string ad, Kategori? ust)
        {
            var seviye = (ust?.Seviye ?? 0) + 1;

            if ((seviye > MaksimumSeviye))
            {
                throw new DomainException($"Kategori derinliği en fazla {MaksimumSeviye} olabilir");
            }
            var kategori = new Kategori
            {
                UstKategori = ust,
                UstKategoriId = ust?.Id,
                Seviye = seviye
            };
            kategori.AdDegistir(ad);
            return kategori;
        }

        private void AdDegistir(string ad)
        {
            if (string.IsNullOrWhiteSpace(ad))
            {
                throw new DomainException("Kategori Adı boş olamaz");
            }

            Ad = ad;
        }

        public void YolHesapla()
        {
            if (Id == 0)
            {

                throw new InvalidOperationException("Yol hesaplanmadan önce Id atanmış olmalı");
            }

            Yol = (UstKategori?.Yol ?? "/") + $"{Id}/";

        }

        public void Tasi(Kategori? yeniUst, IReadOnlyCollection<Kategori> altAgac)        {
            if(yeniUst is not null && yeniUst.Yol.StartsWith(Yol, StringComparison.Ordinal))
            
                throw new DomainException("Bir Kategori Kendi Altına Taşınamaz");
            

            var yeniSeviye = (yeniUst?.Seviye ?? 0) + 1;
            var fark = yeniSeviye - Seviye;
            var enDerinSeviye = altAgac.Count == 0 ? Seviye : altAgac.Max(k => k.Seviye);
            if (enDerinSeviye + fark > MaksimumSeviye)
                throw new DomainException($"Taşıma sonrası derinlik {MaksimumSeviye} seviyesinden fazla olamaz");

            var eskiYol = Yol;
            UstKategori = yeniUst;
            UstKategoriId = yeniUst?.Id;
            Seviye = yeniSeviye;
            Yol = (yeniUst?.Yol ?? "/") + $"{Id}/";
            foreach (var alt in altAgac)
            {
                alt.Yol = Yol + alt.Yol[eskiYol.Length..];
                alt.Seviye += fark;
            }

        }


    }
}
