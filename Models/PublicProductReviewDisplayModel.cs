using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class PublicProductReviewDisplayModel : BaseNopModel
    {
        public PublicProductReviewDisplayModel()
        {
            ProductImageUrl = new Dictionary<int, string>();
        }
        public IList<ProductReviewListModel> ProductReviews { get; set; }
        public IDictionary<int, string> ProductImageUrl { get; set; }
    }
}
