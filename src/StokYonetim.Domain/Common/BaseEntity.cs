using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; } = [];
    }
}
