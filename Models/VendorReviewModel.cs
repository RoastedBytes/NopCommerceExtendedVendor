using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class VendorReviewModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Vendor")]
        public int VendorId
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Customer")]
        public int CustomerId
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Product")]
        public int ProductId
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.IsApproved")]
        public bool IsApproved
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Title")]
        public string Title
        {
            get;
            set;
        }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.ReviewText")]
        public string ReviewText
        {
            get;
            set;
        }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Rating")]
        public int Rating
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.HelpfulnessYesTotal")]
        public int HelpfulnessYesTotal
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.HelpfulnessNoTotal")]
        public int HelpfulnessNoTotal
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.CreatedOnUTC")]
        public DateTime CreatedOnUTC
        {
            get;
            set;
        }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Order")]
        public int OrderId
        {
            get;
            set;
        }
      
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.Product")]
        public string ProductName
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.CertifiedBuyerReview")]
        public bool CertifiedBuyerReview
        {
            get;
            set;
        }
        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.DisplayCertifiedBadge")]
        public bool DisplayCertifiedBadge
        {
            get;
            set;
        }

    }
}
