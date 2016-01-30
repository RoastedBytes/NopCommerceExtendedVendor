using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain
{
    public class VendorReview : BaseEntity
    {
        public virtual int VendorId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int OrderId {get; set;}
        public virtual bool IsApproved { get; set; }
        public virtual string Title { get; set; }
        public virtual string ReviewText { get; set; }
        public virtual int Rating { get; set; }
        public virtual int HelpfulnessYesTotal { get; set; }
        public virtual int HelpfulnessNoTotal { get; set; }
        public virtual DateTime CreatedOnUTC { get; set; }
        public virtual bool CertifiedBuyerReview {get;set;}
        public virtual bool DisplayCertifiedBadge
        {
            get;
            set;
        }
        
    }
}
