using StokYonetim.Domain.Common;
using StokYonetim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public class StokHareketi:BaseEntity
    {
        public HareketTipi Tip { get; private set; }
        public int UrunId { get; private set; }
        public Urun Urun { get; private set; } = null!;
        public int DepoId { get; private set; }
        public Depo Depo { get; private set; } = null!;

        public decimal Miktar { get; private set; }
        public decimal? BirimFiyat { get; private set; }
        public int? TederikciId { get; private set; }
        public Tedarikci Tedarikci { get; private set; }
        public Guid? TransferId { get; private set; }
        public DateOnly IslemTarihi { get; private set; }
        public DateTimeOffset OlusturmaTarihi { get; private set; }

        public string? Aciklama { get; private set; }

        public bool AzaltanMi => Tip is HareketTipi.Cikis or HareketTipi.TransferCikis or HareketTipi.SayimEksigi;
        public decimal IsaretliMiktar => AzaltanMi ? -Miktar : Miktar;
        private StokHareketi() { }

        public static StokHareketi Olustur(
            HareketTipi tip,
            Urun urun,
            int depoId,
            decimal miktar,
            DateOnly islemTarihi,
            decimal? birimFiyat=null,
            Guid? transferId=null,
            string? aciklama=null
            )
        {
            if (miktar <= 0)
                throw new DomainException("Miktar sıfırdan büyük olmalı");
            if (decimal.Round(miktar, 3) != miktar)
                throw new DomainException("Miktar en fazla 3 ondalık basamak içerebilir");

            var transferMi = tip is HareketTipi.TransferGiris or HareketTipi.TransferCikis;
            if (transferMi != transferId.HasValue)
                throw new DomainException("Transfer haareketi ile transfer id uyuşmuyor");





            return new StokHareketi
            {
                Tip = tip,
                UrunId = urun.Id,
                DepoId = depoId,
                Miktar = miktar,
                IslemTarihi = islemTarihi,
                BirimFiyat = birimFiyat,
                TransferId = transferId,
                Aciklama = aciklama,
                TederikciId = tip == HareketTipi.Giris ? urun.TedarikciId : null
            };
        }







    }
}
