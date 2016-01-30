using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain
{
    public class ExtendedVendor : BaseEntity
    {
        public virtual int VendorId { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode
        {
            get;
            set;
        }
        public virtual string PhoneNumber
        {
            get;
            set;
        }
        public virtual string TinNumber
        {
            get;
            set;
        }
        public virtual string ServiceTaxNumber
        {
            get;
            set;
        }
        public virtual string ShortCode
        {
            get;
            set;
        }
        public virtual string VatCST
        {
            get;
            set;
        }
        public virtual int StateProvinceId { get; set; }
        public virtual int CountryId { get; set; }
        public virtual bool ReviewsEnabled { get; set; }        
        public virtual bool HelpfulnessEnabled { get; set; }
        public virtual int LogoId { get; set; }

        public virtual decimal CommissionPercentage {get;set;}
        public virtual decimal ShippingCharge { get; set; }
    }
}
