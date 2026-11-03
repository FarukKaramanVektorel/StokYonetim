using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Domain.Common
{
    public class DomainException(string message) : Exception(message);
}
