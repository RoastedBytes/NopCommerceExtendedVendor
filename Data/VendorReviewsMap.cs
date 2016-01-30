using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Data
{
    public class VendorReviewsMap : EntityTypeConfiguration<VendorReview>
    {
        public VendorReviewsMap()
        {
            ToTable("ApexolVendorReviews");
            Property(m => m.Id);
            Property(m => m.CreatedOnUTC);
            Property(m => m.CustomerId);
            Property(m => m.HelpfulnessNoTotal);
            Property(m => m.HelpfulnessYesTotal);
            Property(m => m.IsApproved);
            Property(m => m.ProductId);
            Property(m => m.Rating);
            Property(m => m.ReviewText);
            Property(m => m.Title);
            Property(m => m.VendorId);
            Property(m => m.OrderId);
            Property(m => m.CertifiedBuyerReview);
            Property(m => m.DisplayCertifiedBadge);
        }
    }
}
