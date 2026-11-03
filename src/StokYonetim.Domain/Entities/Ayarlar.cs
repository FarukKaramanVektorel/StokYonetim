using StokYonetim.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Entities
{
    public class Ayarlar:BaseEntity
    {
        public DateOnly? KilitTarihi { get; set; }
    }
}
