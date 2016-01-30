using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class ExtendedVendorModel : BaseNopEntityModel
    {

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Vendor")]
        public int VendorId { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.AddressLine1")]
        public string AddressLine1 { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.AddressLine2")]
        public string AddressLine2 { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.City")]
        public string City { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.StateProvince")]
        public int StateProvinceId { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Country")]
        public int CountryId { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.ReviewsEnabled")]
        public bool ReviewsEnabled { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.HelpfulnessEnabled")]
        public bool HelpfulnessEnabled { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.LogoId")]
        [UIHint("Picture")]
        public int LogoId { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.TinNumber")]
        public string TinNumber
        {
            get;
            set;
        }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.ServiceTaxNumber")]
        public string ServiceTaxNumber
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.ShortCode")]
        public string ShortCode
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.VatCST")]
        public string VatCST
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.ZipCode")]
        public string ZipCode
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.PhoneNumber")]
        public string PhoneNumber
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.CommissionPercentage")]
        public decimal CommissionPercentage
        {
            get;
            set;
        }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.DefaultShippingCharge")]
        public decimal ShippingCharge
        {
            get;
            set;
        }

    }
}
