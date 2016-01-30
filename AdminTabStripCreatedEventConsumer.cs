using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Services;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Core.Infrastructure;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public class AdminTabStripCreatedEventConsumer : IConsumer<AdminTabStripCreated>
    {

        public void HandleEvent(AdminTabStripCreated eventMessage)
        {
            if (eventMessage.TabStripName != "vendor-edit") 
                return;

            var evc = EngineContext.Current.Resolve<ExtendedVendorController>();

            var vendorId = evc.GetCurrentVendorId();
            var content = ViewRenderer.RenderPartialView("ExtendedVendor/_TabEditVendor", vendorId);
            eventMessage.BlocksToRender.Add(MvcHtmlString.Create(content));

            content = ViewRenderer.RenderPartialView("ExtendedVendor/_TabViewReviews", vendorId);
            eventMessage.BlocksToRender.Add(MvcHtmlString.Create(content));

            content = ViewRenderer.RenderPartialView("ExtendedVendor/_TabViewPayouts", vendorId);
            eventMessage.BlocksToRender.Add(MvcHtmlString.Create(content));
        }
    }
}
