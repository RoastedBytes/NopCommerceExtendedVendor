using Nop.Core.Domain.Discounts;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class PublicReviewEditModel : BaseNopEntityModel
    {
        public AddProductReviewModel AddProductReviewModel
        {
            get;
            set;
        }
        public VendorReviewListModel VendorReviewListModel
        {
            get;
            set;

        }
        public string VendorName
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public string ProductImageUrl
        {
            get;
            set;
        }
        public string VendorImageUrl
        {
            get;set;
        }

        public string ProductSeName
        {
            get;
            set;
        }
        public string VendorSeName
        {
            get;
            set;
        }
    }
}
