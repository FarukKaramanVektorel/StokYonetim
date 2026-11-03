namespace StokYonetim.Application.DTOs;

public class UrunCreateDto
{
    public string Ad { get; set; } = string.Empty;
    public string Barkod { get; set; } = string.Empty;
    public decimal AlisFiyati { get; set; }
    public decimal SatisFiyati { get; set; }
    public int KategoriId { get; set; }
    public int Birim { get; set; } // Enum karşılığı
}