using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Enums;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Services
{
    public interface IExtendedVendorService
    {
        ExtendedVendor.Domain.ExtendedVendor GetExtendedVendor(int VendorId);

        VendorPayout GetVendorPayout(int VendorPayoutId);

        VendorReview GetVendorReview(int VendorReviewId);

        VendorReview GetVendorReview(int VendorId, int CustomerId, int OrderId, int ProductId);

        IPagedList<VendorReview> GetVendorReviews(int? VendorId, int? CustomerId, bool? IsApproved, int PageNumber = 1, int PageSize = int.MaxValue);

        IPagedList<VendorPayout> GetVendorPayouts(int VendorId, PayoutStatus? PayoutStatus, int PageNumber = 1, int PageSize = int.MaxValue);

        IList<VendorPayout> GetVendorPayoutsByOrder(int OrderId);

        void SaveExtendedVendor(ExtendedVendor.Domain.ExtendedVendor ExtendedVendor);

        void SaveVendorReview(VendorReview VendorReview);

        void SaveVendorPayout(VendorPayout VendorPayout);

        void DeleteVendorReview(VendorReview VendorReview);

        void DeleteVendorPayout(VendorPayout VendorPayout);

        bool IsVendorReviewDone(int VendorId, int CustomerId, int OrderId, int ProductId);

        bool CanCustomerReviewVendor(int VendorId, int CustomerId, int OrderId);

        Dictionary<Order, List<Product>> GetProductsWithPendingReviews(IList<Order> Orders, int CustomerId);
    }
}
