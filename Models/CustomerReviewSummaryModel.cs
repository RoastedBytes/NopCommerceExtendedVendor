using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class CustomerReviewSummaryModel : BaseNopModel
    {
        public CustomerReviewSummaryModel()
        {
            ProductRatingCounts = new int[5];
            VendorRatingCounts = new int[5];
        }
        public int TotalProductReviews { get; set; }
        public int TotalVendorReviews { get; set; }
        public int[] ProductRatingCounts { get; set; }
        public int[] VendorRatingCounts { get; set; }
        public ProductReview LastRatedProductReview { get; set; }
        public VendorReview LastRatedVendorReview { get; set; }
        public string CustomerName { get; set; }
        public bool IsRedirection { get; set; }
    }
}
