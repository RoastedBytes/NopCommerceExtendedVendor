using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.ViewEngines;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Seo;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
           
            System.Web.Mvc.ViewEngines.Engines.Add(new ExtendedVendorViewEngine());
           

            routes.MapRoute("ExtendedVendorEditVendor",
                           "extendedvendor/vendor/edit/{Id}",
                           new { controller = "ExtendedVendor", action = "EditVendor", Id = 0 },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

            routes.MapRoute("ExtendedVendorSaveVendor",
                           "extendedvendor/vendor/save",
                           new { controller = "ExtendedVendor", action = "SaveVendor" },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

            routes.MapRoute("ExtendedVendorEditReview",
                           "extendedvendor/vendor/review/edit/{Id}",
                           new { controller = "ExtendedVendor", action = "EditReview", Id = 0 },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

          

            routes.MapRoute("ExtendedVendorListReviewsAdmin",
                           "extendedvendor/vendor/review/list/{VendorId}",
                           new { controller = "ExtendedVendor", action = "ListReviewsAdmin", VendorId = 0 },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

            routes.MapRoute("ExtendedVendorDetails",
                           "vendor-details/{SeName}",
                           new { controller = "ExtendedVendor", action = "ExtendedVendorDetails"},
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" });

            routes.MapRoute("ExtendedVendorListPayoutsAdmin",
                           "extendedvendor/vendor/payout/list/{VendorId}",
                           new { controller = "ExtendedVendor", action = "ListPayoutsAdmin", VendorId = 0 },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

            routes.MapRoute("ExtendedVendorEditPayout",
                           "extendedvendor/vendor/payout/edit/{Id}",
                           new { controller = "ExtendedVendor", action = "EditPayout", Id = 0 },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

            routes.MapRoute("ExtendedVendorSavePayout",
                           "extendedvendor/vendor/payout/save",
                           new { controller = "ExtendedVendor", action = "SavePayout" },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }).DataTokens.Add("area", "Admin");

            routes.MapRoute("ExtendedVendorEditReviewPublic",
                           "edit-extended-review/{OrderId}/{ProductId}",
                           new
                           {
                               controller = "ExtendedVendor",
                               action = "EditExtendedReview",
                           },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" });

            routes.MapRoute("ExtendedVendorReviewCenterLoader",
                           "customer/review-center",
                           new
                           {
                               controller = "ExtendedVendor",
                               action = "UserReviews",                               
                           },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" });

            routes.MapRoute("ExtendedVendorReviewCenter",
                           "account/review-center/{Type}",
                           new
                           {
                               controller = "ExtendedVendor",
                               action = "UserReviewsData",
                               Type = "product",
                           },
                           new[] { "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" });


        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
