using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Data;
using Nop.Core.Plugins;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Nop.Services.Localization;
using Nop.Services.Cms;
using Nop.Web.Framework.Menu;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public class ExtendedVendorPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly ISettingService _settingService;
        private readonly ExtendedVendorObjectContext _objectContext;
        private readonly ICategoryService _categoryService;
        public ExtendedVendorPlugin(ISettingService settingService, ExtendedVendorObjectContext objectContext, ICategoryService categoryService)
        {
            this._settingService = settingService;
            this._objectContext = objectContext;
            this._categoryService = categoryService;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ExtendedVendor";
            routeValues = new RouteValueDictionary() { { "Namespaces", "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }, { "area", null } };
        }

        public override void Install()
        {
            base.Install();
            _objectContext.Install();

            var resources = new Dictionary<string, string>
            {
                {"Edit", "Edit"},
                {"Store", ""},
                {"Store.Hint", ""},
                {"Vendor", ""},
                {"Vendor.Hint", ""},
                {"AddressLine1", ""},
                {"AddressLine1.Hint", ""},
                {"AddressLine2", ""},
                {"AddressLine2.Hint", ""},
                {"City", ""},
                {"City.Hint", ""},
                {"StateProvince", ""},
                {"StateProvince.Hint", ""},
                {"Country", ""},
                {"Country.Hint", ""},
                {"ReviewsEnabled", "Reviews Enabled?"},
                {"ReviewsEnabled.Hint", "Should reviews be enabled for this vendor?"},
                {"HelpfulnessEnabled", "Helpfulness Enabled?"},
                {"HelpfulnessEnabled.Hint", "Should helpfulness for reviews be enabled(if reviews are enabled)?"},
                {"Logo", ""},
                {"Logo.Hint", ""},
                {"ServiceTaxNumber", ""},
                {"ServiceTaxNumber.Hint", ""},
                {"TinNumber", ""},
                {"TinNumber.Hint", ""},
                {"ShortCode", ""},
                {"ShortCode.Hint", "Code that may be used for generating order numbers / invoice numbers"},
                {"VatCST", ""},
                {"VatCST.Hint", ""},
                {"ZipCode", ""},
                {"ZipCode.Hint", ""},
                {"PhoneNumber", ""},
                {"PhoneNumber.Hint", ""},
                {"Order", ""},
                {"Order.Hint", ""},
                {"VendorOrderTotal", "Vendor Order Total"},
                {"VendorOrderTotal.Hint", "Vendor Order Total"},
                {"CommissionPercentage", "Commission Percentage"},
                {"CommissionPercentage.Hint", "The commission percentage that must be deducted for your store"},
                {"PayoutStatus", "Payout Status"},
                {"PayoutStatus.Hint", "The status of payout if it's paid to vendor or not"},
                {"PayoutDate", "Payout Date"},
                {"PayoutDate.Hint", "Payout Date"},
                {"Remarks", ""},
                {"Remarks.Hint", ""},
                {"Customer", ""},
                {"Customer.Hint", ""},
                {"Product", ""},
                {"Product.Hint", ""},
                {"IsApproved", "Approve"},
                {"IsApproved.Hint", "Approve"},
                {"Title", ""},
                {"Title.Hint", ""},
                {"Rating", ""},
                {"Rating.Hint", ""},
                {"HelpfulnessYesTotal", "Helpful (yes)"},
                {"HelpfulnessYesTotal.Hint", "Helpful (yes)"},
                {"HelpfulnessNoTotal", "Helpful (no)"},
                {"HelpfulnessNoTotal.Hint", "Helpful (no)"},
                {"CreatedOnUTC", "Created On"},
                {"CreatedOnUTC.Hint", "Created On"},
                {"ReviewText", "Review Text"},
                {"ReviewText.Hint", "Review Text"},
                {"CommissionAmount", "Commission Amount"},
                {"PayoutAmount", "Payout Amount"},
                {"DefaultCommissionPercentage", "Default Commission Percentage"},
                {"DefaultShippingCharge", "Shipping Charge Per Product"}

            };
            foreach (var kp in resources)
            {
                var key = "Plugin.Misc.ExtendedVendor.Fields." + kp.Key;
                var value = kp.Value;
                this.AddOrUpdatePluginLocaleResource(key, value);
            }

        }
        public override void Uninstall()
        {
            base.Uninstall();
            _objectContext.Uninstall();
            var resources = new Dictionary<string, string>
            {
                {"Edit", "Edit"},
                {"Store", ""},
                {"Store.Hint", ""},
                {"Vendor", ""},
                {"Vendor.Hint", ""},
                {"AddressLine1", ""},
                {"AddressLine1.Hint", ""},
                {"AddressLine2", ""},
                {"AddressLine2.Hint", ""},
                {"City", ""},
                {"City.Hint", ""},
                {"StateProvince", ""},
                {"StateProvince.Hint", ""},
                {"Country", ""},
                {"Country.Hint", ""},
                {"ReviewsEnabled", "Reviews Enabled?"},
                {"ReviewsEnabled.Hint", "Should reviews be enabled for this vendor?"},
                {"HelpfulnessEnabled", "Helpfulness Enabled?"},
                {"HelpfulnessEnabled.Hint", "Should helpfulness for reviews be enabled(if reviews are enabled)?"},
                {"Logo", ""},
                {"Logo.Hint", ""},
                {"ServiceTaxNumber", ""},
                {"ServiceTaxNumber.Hint", ""},
                {"TinNumber", ""},
                {"TinNumber.Hint", ""},
                {"ShortCode", ""},
                {"ShortCode.Hint", "Code that may be used for generating order numbers / invoice numbers"},
                {"VatCST", ""},
                {"VatCST.Hint", ""},
                {"ZipCode", ""},
                {"ZipCode.Hint", ""},
                {"PhoneNumber", ""},
                {"PhoneNumber.Hint", ""},
                {"Order", ""},
                {"Order.Hint", ""},
                {"VendorOrderTotal", "Vendor Order Total"},
                {"VendorOrderTotal.Hint", "Vendor Order Total"},
                {"CommissionPercentage", ""},
                {"CommissionPercentage.Hint", "The commission percentage that must be deducted for your store"},
                {"PayoutStatus", ""},
                {"PayoutStatus.Hint", "The status of payout if it's paid to vendor or not"},
                {"PayoutDate", ""},
                {"PayoutDate.Hint", ""},
                {"Remarks", ""},
                {"Remarks.Hint", ""},
                {"Customer", ""},
                {"Customer.Hint", ""},
                {"Product", ""},
                {"Product.Hint", ""},
                {"IsApproved", "Approve"},
                {"IsApproved.Hint", "Approve"},
                {"Title", ""},
                {"Title.Hint", ""},
                {"Rating", ""},
                {"Rating.Hint", ""},
                {"HelpfulnessYesTotal", "Helpful (yes)"},
                {"HelpfulnessYesTotal.Hint", "Helpful (yes)"},
                {"HelpfulnessNoTotal", "Helpful (no)"},
                {"HelpfulnessNoTotal.Hint", "Helpful (no)"},
                {"CreatedOnUTC", "Created On"},
                {"CreatedOnUTC.Hint", "Created On"},
                {"ReviewText", "Review Text"},
                {"ReviewText.Hint", "Review Text"},
                {"CommissionAmount", "Commission Amount"},
                {"PayoutAmount", "Payout Amount"},
                {"DefaultCommissionPercentage", "Default Commission Percentage"},
                {"DefaultShippingCharge", "Shipping Charge Per Product"}

            };
            foreach (var kp in resources)
            {
                string key = "Plugin.Misc.ExtendedVendor.Fields." + kp.Key;
                this.DeletePluginLocaleResource(key);
            }

        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                "account_navigation_after",
                "productreviews_page_inside_review",
                "vendordetails_top"
            };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "";
            controllerName = "ExtendedVendor";
            routeValues = new RouteValueDictionary() { { "Namespaces", "RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers" }, { "area", null } };

            switch (widgetZone)
            {
                
                case "vendordetails_top":
                    actionName = "VendorRatingHandler";
                    break;
                case "account_navigation_after":
                    actionName = "DisplayNavigation";
                    break;
                case "productreviews_page_inside_review":
                    actionName = "CertifiedBadgeHandler";
                    break;

            }
            

        }

        public SiteMapNode BuildMenuItem()
        {
            var menuItemBuilder = new SiteMapNode() {
                Title = "Extended Vendor & Ratings",   // Title for your Custom Menu Item
                SystemName = "RoastedBytes.Plugin.ExtendedVendor",
                Url = "", // Path of the action link
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "Area", "Admin" } }
            };

            var SubMenuItem = new SiteMapNode()   // add child Custom menu
               {
                   Title = "Configure", //   Title for your Sub Menu item
                   Url = "~/Admin/Widget/ConfigureWidget?systemName=RoastedBytes.Plugin.ExtendedVendor",
                   Visible = true,
                   RouteValues = new RouteValueDictionary() { { "Area", "Admin" } },
               };
            menuItemBuilder.ChildNodes.Add(SubMenuItem);

            SubMenuItem = new SiteMapNode()   // add child Custom menu
                {
                    Title = "Help", //   Title for your Sub Menu item
                    Visible = true,
                    Url = "http://docs.roastedbytes.com/",
                    RouteValues = new RouteValueDictionary() { { "Area", "Admin" } },
                };
            menuItemBuilder.ChildNodes.Add(SubMenuItem);

            return menuItemBuilder;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var menuItem = BuildMenuItem();
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);
        }
    }
}
