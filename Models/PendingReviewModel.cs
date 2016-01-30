using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class PendingReviewModel : BaseNopModel
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string ProductSeName { get; set; }
        public string ProductImageUrl { get; set; }
        public string VendorName { get; set; }
        public string VendorSeName { get; set; }
        public string VendorImageUrl { get; set; }
        public int ProductId { get; set; }

    }
}
