using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Enums;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain
{
    public class VendorPayout : BaseEntity
    {
        public virtual int VendorId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual decimal VendorOrderTotal { get; set; }
        public virtual decimal CommissionPercentage { get; set; }
        public virtual PayoutStatus PayoutStatus { get; set; }
        public virtual DateTime? PayoutDate { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal ShippingCharge { get; set; }
    }
}