using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class VendorReviewSummaryModel: BaseNopModel
    {
        public VendorReviewSummaryModel()
        {
            VendorRatingCounts = new int[5];
        }
        public int TotalVendorReviews { get; set; }
        public int[] VendorRatingCounts { get; set; }
        public string VendorName { get; set; }

        public PublicVendorReviewDisplayModel VendorReviewDisplayModel { get; set; }
    }
}