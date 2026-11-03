using StokYonetim.Application.DTOs;

namespace StokYonetim.Application.Interfaces
{
    public interface IUrunService
    {
        Task<IEnumerable<UrunDto>> TumUrunleriGetirAsync();
        Task<UrunDto?> IdyeGoreUrunGetirAsync(int id);
        Task UrunEkleAsync(UrunCreateDto yeniUrunDto);
        Task UrunGuncelleAsync(UrunDto urunDto);
        Task UrunSilAsync(int id);
    }
}