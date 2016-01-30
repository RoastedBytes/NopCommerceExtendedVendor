using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class VendorRatingModel : BaseNopModel
    {
        public int TotalRatings { get; set; }

        public decimal AverageRating { get; set; }

        public int VendorId { get; set; }

        public string VendorSeName { get; set; }
    }
}
