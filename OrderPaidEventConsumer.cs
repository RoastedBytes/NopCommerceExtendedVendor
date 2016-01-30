using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Services;
using Nop.Core.Domain.Orders;
using Nop.Services.Discounts;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Discounts;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using Nop.Core.Domain.Tax;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Enums;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public class OrderPaidEventConsumer : IConsumer<OrderPaidEvent>
    {
        private readonly IExtendedVendorService _extendedVendorService;
        private readonly TaxSettings _taxSettings;
        private readonly ExtendedVendorSettings _extendedVendorSettings;

        public OrderPaidEventConsumer(IExtendedVendorService extendedVendorService, TaxSettings taxSettings, ExtendedVendorSettings extendedVendorSettings)
        {
            this._extendedVendorService = extendedVendorService;
            this._taxSettings = taxSettings;
            this._extendedVendorSettings = extendedVendorSettings;
        }
        public void HandleEvent(OrderPaidEvent eventMessage)
        {
            var order = eventMessage.Order;
            var vendorIds = order.OrderItems.Select(m => m.Product.VendorId).Distinct();
           
            //to store the vendor based order items 
            var vendorItems = new Dictionary<int, IList<OrderItem>>();
            //to store the vendors in a dictionary because there may be more than one vendors in one order
            var vendorsExtended = new Dictionary<int, Domain.ExtendedVendor>();
            
            foreach (var vid in vendorIds)
            {
                vendorItems.Add(vid, order.OrderItems.Where(m => m.Product.VendorId == vid).ToList());
                var ex = _extendedVendorService.GetExtendedVendor(vid);
                vendorsExtended.Add(vid, ex);
            }
            
            foreach (var vi in vendorItems)
            {
                var vendorId = vi.Key;
                var orderItems = vi.Value;

                var orderItemTotal = decimal.Zero;
                var discountTotal = decimal.Zero;
                if (_taxSettings.PricesIncludeTax)
                {
                    orderItemTotal = orderItems.Sum(m => m.PriceInclTax);
                    discountTotal = orderItems.Sum(m => m.DiscountAmountInclTax);
                }
                else
                {
                    orderItemTotal = orderItems.Sum(m => m.PriceExclTax);
                    discountTotal = orderItems.Sum(m => m.DiscountAmountExclTax);
                }
                orderItemTotal = orderItemTotal - discountTotal;

                //create a new payout for each vendor
                var vendorPayout = new VendorPayout
                {
                    VendorId = vendorId,
                    CommissionPercentage =
                        vendorsExtended[vendorId] == null
                            ? _extendedVendorSettings.DefaultCommissionPercentage
                            : vendorsExtended[vendorId].CommissionPercentage,
                    OrderId = order.Id,
                    PayoutDate = null,
                    Remarks = "",
                    VendorOrderTotal = orderItemTotal,
                    PayoutStatus = PayoutStatus.Pending,
                    ShippingCharge =
                        (vendorsExtended[vendorId] == null
                            ? _extendedVendorSettings.DefaultShippingCharge
                            : vendorsExtended[vendorId].ShippingCharge)*orderItems.Count
                };

                _extendedVendorService.SaveVendorPayout(vendorPayout);
            }
            
        }
    }
}
