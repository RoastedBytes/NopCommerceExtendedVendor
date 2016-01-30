using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Enums;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Services
{
    public class ExtendedVendorService : IExtendedVendorService
    {
        private readonly IRepository<Domain.ExtendedVendor> _extendedVendorRepository;
        private readonly IRepository<VendorPayout> _vendorPayoutRepository;
        private readonly IRepository<VendorReview> _vendorReviewRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IOrderService _orderService;

        public ExtendedVendorService(IRepository<Domain.ExtendedVendor> extendedVendorRepository,
                            IRepository<VendorPayout> vendorPayoutRepository,
                            IRepository<VendorReview> vendorReviewRepository,
                            ICacheManager cacheManager,
                            IOrderService orderService)
        {
            this._extendedVendorRepository = extendedVendorRepository;
            this._vendorPayoutRepository = vendorPayoutRepository;
            this._vendorReviewRepository = vendorReviewRepository;
            this._cacheManager = cacheManager;
            this._orderService = orderService;
        }

        public Domain.ExtendedVendor GetExtendedVendor(int VendorId)
        {
            return _extendedVendorRepository.Table.FirstOrDefault(x => x.VendorId == VendorId);
        }

        public IPagedList<VendorReview> GetVendorReviews(int? VendorId, int? CustomerId, bool? IsApproved, int PageNumber = 1, int PageSize = int.MaxValue)
        {
            var query = _vendorReviewRepository.Table;
            if (VendorId.HasValue)
                query = query.Where(x => x.VendorId == VendorId);

            if (CustomerId.HasValue)
                query = query.Where(x => x.CustomerId == CustomerId);

            if (IsApproved.HasValue)
                query = query.Where(x => x.IsApproved == IsApproved);

            //reverse sort
            var records = query.OrderByDescending(r => r.CreatedOnUTC).ToList();
            var paged = new PagedList<VendorReview>(records, PageNumber - 1, PageSize);
            return paged;
        }


        public IPagedList<VendorPayout> GetVendorPayouts(int VendorId, PayoutStatus? PayoutStatus, int PageNumber = 1, int PageSize = int.MaxValue)
        {
            var key = "VENDOR-PAYOUT-{0}-{1}";
            key = PayoutStatus.HasValue ? string.Format(key, VendorId, PayoutStatus.Value) : string.Format(key, VendorId, "all");

            return _cacheManager.Get(key, () =>
            {
                var query = _vendorPayoutRepository.Table.Where(x => x.VendorId == VendorId);
                if (PayoutStatus.HasValue)
                    query = query.Where(x => x.PayoutStatus == PayoutStatus.Value);

                var records = query.ToList();
                var paged = new PagedList<VendorPayout>(records, PageNumber - 1, PageSize);
                return paged;
            });
        }


        public IList<VendorPayout> GetVendorPayoutsByOrder(int OrderId)
        {
            var query = _vendorPayoutRepository.Table.Where(x => x.OrderId == OrderId);
            return query.ToList();
        }


        public void SaveExtendedVendor(Domain.ExtendedVendor ExtendedVendor)
        {
            if (ExtendedVendor.Id == 0)
                _extendedVendorRepository.Insert(ExtendedVendor);
            else
                _extendedVendorRepository.Update(ExtendedVendor);
        }

        public void SaveVendorReview(VendorReview VendorReview)
        {
            if (VendorReview.Id == 0)
                _vendorReviewRepository.Insert(VendorReview);
            else
                _vendorReviewRepository.Update(VendorReview);
        }

        public void SaveVendorPayout(VendorPayout VendorPayout)
        {
            if (VendorPayout.Id == 0)
                _vendorPayoutRepository.Insert(VendorPayout);
            else
                _vendorPayoutRepository.Update(VendorPayout);
        }


        public VendorPayout GetVendorPayout(int VendorPayoutId)
        {
            return _vendorPayoutRepository.Table.FirstOrDefault(x => x.Id == VendorPayoutId);
        }

        public VendorReview GetVendorReview(int VendorReviewId)
        {
            return _vendorReviewRepository.Table.FirstOrDefault(x => x.Id == VendorReviewId);
        }


        public void DeleteVendorReview(VendorReview VendorReview)
        {
            _vendorReviewRepository.Delete(VendorReview);
        }

        public void DeleteVendorPayout(VendorPayout VendorPayout)
        {
            _vendorPayoutRepository.Delete(VendorPayout);
        }


        public bool IsVendorReviewDone(int VendorId, int CustomerId, int OrderId, int ProductId)
        {
            return
                _vendorReviewRepository.Table.Any(
                    x => x.OrderId == OrderId && x.VendorId == VendorId && x.ProductId == ProductId && x.CustomerId == CustomerId);
        }

        public void CheckVendorReviewAndHelpfulnessEnabled(int VendorId, out bool ReviewEnabled, out bool HelpfulnessEnabled)
        {
            var vendor = GetExtendedVendor(VendorId);
            if (vendor != null)
            {
                ReviewEnabled = vendor.ReviewsEnabled;
                HelpfulnessEnabled = vendor.HelpfulnessEnabled;
            }
            else
            {
                ReviewEnabled = false;
                HelpfulnessEnabled = false;
            }
        }


        public bool CanCustomerReviewVendor(int VendorId, int CustomerId, int OrderId)
        {
            var order = _orderService.GetOrderById(OrderId);
            if (order == null)
                return false;

            var vendorIds = order.OrderItems.ToList().Select(m => m.Product.VendorId);
            return vendorIds.Contains(VendorId) && CustomerId == order.Customer.Id;
        }


        public VendorReview GetVendorReview(int VendorId, int CustomerId, int OrderId, int ProductId)
        {
            var query = from tr in _vendorReviewRepository.Table
                        where tr.VendorId == VendorId && tr.CustomerId == CustomerId && tr.ProductId == ProductId && tr.OrderId == OrderId
                        select tr;
            return query.FirstOrDefault();
        }
      

        public Dictionary<Order, List<Product>> GetProductsWithPendingReviews(IList<Order> Orders, int CustomerId)
        {
            var dict = new Dictionary<Order, List<Product>>();
            var customerReviews = GetVendorReviews(null, CustomerId, null);

            foreach (var order in Orders)
            {
                var orderReviews = customerReviews.Where(cr => cr.OrderId == order.Id);
                if (!orderReviews.Any())
                    dict.Add(order, order.OrderItems.Select(oi => oi.Product).ToList());
                else
                {
                    var orderProductIds = order.OrderItems.Select(oi => oi.Product.Id).ToList();
                    var reviewedProductIds = orderReviews.Select(or => or.ProductId).ToList();
                    if (orderProductIds.Count == reviewedProductIds.Count)
                        continue; //all products reviewed for this order

                    var pendingForReviewIds = orderProductIds.Except(reviewedProductIds);
                    var pendingProducts = order.OrderItems.Where(oi => pendingForReviewIds.Contains(oi.ProductId))
                                            .Select(oi => oi.Product).ToList();

                    dict.Add(order, pendingProducts);
                }
            }
            return dict;

        }




    }
}
