using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public class ExtendedVendorSettings : ISettings
    {
        public decimal DefaultCommissionPercentage { get; set; }
        public decimal DefaultShippingCharge { get; set; }
    }
}
