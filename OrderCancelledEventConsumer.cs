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
    public class OrderCancelledEventConsumer : IConsumer<OrderCancelledEvent>
    {
        private IExtendedVendorService _extendedVendorService;
        private TaxSettings _taxSettings;

        public OrderCancelledEventConsumer(IExtendedVendorService extendedVendorService, TaxSettings taxSettings)
        {
            this._extendedVendorService = extendedVendorService;
            this._taxSettings = taxSettings;
        }
        public void HandleEvent(OrderCancelledEvent eventMessage)
        {
            var order = eventMessage.Order;
            var payouts = _extendedVendorService.GetVendorPayoutsByOrder(order.Id);

            //mark each payout as cancelled as order has been cancelled
            foreach (var p in payouts)
            {
                p.PayoutStatus = PayoutStatus.Cancelled;
                p.Remarks += " (Order cancelled on " + DateTime.Now.ToString("dd MMM yyyy") + ")";
                _extendedVendorService.SaveVendorPayout(p);
            }

            
        }
    }
}
