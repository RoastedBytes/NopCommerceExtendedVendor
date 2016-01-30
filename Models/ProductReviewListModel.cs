using Nop.Core.Domain.Discounts;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class ProductReviewListModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }
        public string ProductSeName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public int CustomerId { get; set; }

        public string Title { get; set; } 
        public string ProductName
        {
            get;
            set;
        }
        public string ProductImageUrl { get; set; }

        public DateTime CreatedOnUtc { get; set; }
       
    }
}
