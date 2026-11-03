namespace StokYonetim.Application.DTOs;

public class UrunDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Barkod { get; set; } = string.Empty;
    public decimal AlisFiyati { get; set; }
    public decimal SatisFiyati { get; set; }
    public int KategoriId { get; set; }
    public string KategoriAdi { get; set; } = string.Empty;
    public string Birim { get; set; } = string.Empty;
}