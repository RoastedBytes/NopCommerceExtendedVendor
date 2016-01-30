using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            
        }
        public int ActiveStoreScopeConfiguration { get; set; }


        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.DefaultCommissionPercentage")]
        public decimal DefaultCommissionPercentage { get; set; }
        public bool DefaultCommissionPercentage_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugin.Misc.ExtendedVendor.Fields.DefaultShippingCharge")]
        public decimal DefaultShippingCharge { get; set; }
        public bool DefaultShippingCharge_OverrideForStore { get; set; }
    }
}
