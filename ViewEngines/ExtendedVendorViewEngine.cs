using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.ViewEngines
{
    public class ExtendedVendorViewEngine : ThemeableRazorViewEngine
    {
        public ExtendedVendorViewEngine()
        {


            PartialViewLocationFormats = new[]{
                "~/Plugins/RoastedBytes.Misc.ExtendedVendor/Views/{0}.cshtml"
            };
            ViewLocationFormats = new[]{
                "~/Plugins/RoastedBytes.Misc.ExtendedVendor/Views/{0}.cshtml"
            };
        }
    }
}
