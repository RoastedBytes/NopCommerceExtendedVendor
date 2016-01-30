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
    public class PublicPendingReviewDisplayModel : BaseNopModel
    {
        public PublicPendingReviewDisplayModel()
        {
            PendingReviews = new Dictionary<PendingOrderModel, List<PendingReviewModel>>();
        }
        public Dictionary<PendingOrderModel, List<PendingReviewModel>> PendingReviews { get; set; }
       // public Dictionary<Order, List<Product>> PendingReviewProducts { get; set; }
    }
}
