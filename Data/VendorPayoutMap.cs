using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Data
{
    public class VendorPayoutMap : EntityTypeConfiguration<VendorPayout>
    {
        public VendorPayoutMap()
        {
            ToTable("ApexolVendorPayouts");
            Property(m => m.Id);
            Property(m => m.VendorId);
            Property(m => m.CommissionPercentage);
            Property(m => m.OrderId);
            Property(m => m.PayoutDate);
            Property(m => m.PayoutStatus);
            Property(m => m.Remarks);
            Property(m => m.VendorOrderTotal);
            Property(m => m.ShippingCharge);
        }
    }
}
