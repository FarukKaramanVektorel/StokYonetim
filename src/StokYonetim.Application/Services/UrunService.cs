using StokYonetim.Application.DTOs;
using StokYonetim.Application.Interfaces;
using StokYonetim.Domain.Entities;
using StokYonetim.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokYonetim.Application.Services
{
    public class UrunService : IUrunService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UrunService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UrunDto>> TumUrunleriGetirAsync()
        {
            var repository = _unitOfWork.GetRepository<Urun>();
            var urunler = await repository.GetAllAsync();

            // DTO'ya dönüştürme (İleride AutoMapper veya Mapster ile tek satıra indirebiliriz)
            return urunler.Select(u => new UrunDto
            {
                Id = u.Id,
                Ad = u.Ad,
                Barkod = u.Barkod,
                AlisFiyati = u.AlisFiyati,
                SatisFiyati = u.SatisFiyati,
                KategoriId = u.KategoriId,
                Birim = u.Birim.ToString()
            });
        }

        public async Task<UrunDto?> IdyeGoreUrunGetirAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Urun>();
            var u = await repository.GetByIdAsync(id);

            if (u == null) return null;

            return new UrunDto
            {
                Id = u.Id,
                Ad = u.Ad,
                Barkod = u.Barkod,
                AlisFiyati = u.AlisFiyati,
                SatisFiyati = u.SatisFiyati,
                KategoriId = u.KategoriId,
                Birim = u.Birim.ToString()
            };
        }

        public async Task UrunEkleAsync(UrunCreateDto yeniUrunDto)
        {
            var repository = _unitOfWork.GetRepository<Urun>();

            var urun = new Urun
            {
                Ad = yeniUrunDto.Ad,
                Barkod = yeniUrunDto.Barkod,
                AlisFiyati = yeniUrunDto.AlisFiyati,
                SatisFiyati = yeniUrunDto.SatisFiyati,
                KategoriId = yeniUrunDto.KategoriId,
                Birim = (Birim)yeniUrunDto.Birim
            };

            await repository.AddAsync(urun);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UrunGuncelleAsync(UrunDto urunDto)
        {
            var repository = _unitOfWork.GetRepository<Urun>();
            var urun = await repository.GetByIdAsync(urunDto.Id);

            if (urun != null)
            {
                urun.Ad = urunDto.Ad;
                urun.Barkod = urunDto.Barkod;
                urun.AlisFiyati = urunDto.AlisFiyati;
                urun.SatisFiyati = urunDto.SatisFiyati;
                urun.KategoriId = urunDto.KategoriId;

                if (Enum.TryParse<Birim>(urunDto.Birim, out var parsedBirim))
                {
                    urun.Birim = parsedBirim;
                }

                repository.Update(urun);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UrunSilAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Urun>();
            var urun = await repository.GetByIdAsync(id);

            if (urun != null)
            {
                repository.Remove(urun);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}