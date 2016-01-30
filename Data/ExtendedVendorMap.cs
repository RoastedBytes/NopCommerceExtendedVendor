using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Data
{
    public class ExtendedVendorMap : EntityTypeConfiguration<Domain.ExtendedVendor>
    {
        public ExtendedVendorMap()
        {
            ToTable("ApexolExtendedVendor");
            Property(m => m.Id);
            Property(m => m.AddressLine1);
            Property(m => m.AddressLine2);
            Property(m => m.City);
            Property(m => m.CountryId);
            Property(m => m.HelpfulnessEnabled);
            Property(m => m.LogoId);
            Property(m => m.ReviewsEnabled);
            Property(m => m.StateProvinceId);
            Property(m => m.VendorId);
            Property(m => m.VatCST);
            Property(m => m.ServiceTaxNumber);
            Property(m => m.TinNumber);
            Property(m => m.ShortCode);
            Property(m => m.ZipCode);
            Property(m => m.PhoneNumber);
            Property(m => m.CommissionPercentage);
            Property(m => m.ShippingCharge);
        }
    }
}
