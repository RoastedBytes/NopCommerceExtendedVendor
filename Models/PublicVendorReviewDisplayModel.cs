using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using Nop.Core.Domain.Discounts;
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
    public class PublicVendorReviewDisplayModel : BaseNopModel
    {
       
        public IList<VendorReviewListModel> VendorReviews { get; set; }
    }
}
