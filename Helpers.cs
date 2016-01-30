using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models;
using Nop.Core.Domain.Discounts;
using Nop.Services.Discounts;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Services.Seo;
using Nop.Core.Caching;
using Nop.Services.Directory;
using Nop.Services.Customers;
using System.IO;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Vendors;
using Nop.Services.Orders;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Enums;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public static class Helpers
    {
        //TODO: Remove hard coded values to take it from db
        

        static decimal GetPayoutAmount(decimal SellingPrice, decimal CommissionPercentage)
        {

            var total = SellingPrice - (SellingPrice*CommissionPercentage)/100;
            //total = Math.Round(total);


            return total;

        }


        public static ExtendedVendorModel ToModel(this Domain.ExtendedVendor ExtendedVendor)
        {
            var model = new ExtendedVendorModel()
            {
                AddressLine1 = ExtendedVendor.AddressLine1,
                AddressLine2 = ExtendedVendor.AddressLine2,
                City = ExtendedVendor.City,
                CountryId = ExtendedVendor.CountryId,
                HelpfulnessEnabled = ExtendedVendor.HelpfulnessEnabled,
                LogoId = ExtendedVendor.LogoId,
                ReviewsEnabled = ExtendedVendor.ReviewsEnabled,
                StateProvinceId = ExtendedVendor.StateProvinceId,
                VendorId = ExtendedVendor.VendorId,
                TinNumber = ExtendedVendor.TinNumber,
                ServiceTaxNumber = ExtendedVendor.ServiceTaxNumber,
                ShortCode = ExtendedVendor.ShortCode,
                VatCST = ExtendedVendor.VatCST,
                Id = ExtendedVendor.Id,
                ZipCode = ExtendedVendor.ZipCode,
                PhoneNumber = ExtendedVendor.PhoneNumber,
                CommissionPercentage = ExtendedVendor.CommissionPercentage
            };          
            return model;
        }

        public static VendorReviewModel ToModel(this VendorReview Review, Order Order, Product Product)
        {
            var model = new VendorReviewModel()
            {
                CreatedOnUTC = Review.CreatedOnUTC,
                CustomerId = Review.CustomerId,
                HelpfulnessNoTotal = Review.HelpfulnessNoTotal,
                HelpfulnessYesTotal = Review.HelpfulnessYesTotal,
                Id = Review.Id,
                IsApproved = Review.IsApproved,
                ProductId = Review.ProductId,
                Rating = Review.Rating,
                ReviewText = Review.ReviewText,
                Title = Review.Title,
                VendorId = Review.VendorId,
                OrderId = Review.OrderId,
                ProductName = Product.Name,
                CertifiedBuyerReview = Review.CertifiedBuyerReview,
                DisplayCertifiedBadge = Review.DisplayCertifiedBadge
            };
            return model;
        }

        public static VendorReviewListModel ToListModel(this VendorReview Review,IPictureService _pictureService, Product Product, Vendor Vendor, Order Order)
        {
            
            var model = new VendorReviewListModel()
            {
                CreatedOnUTC = Review.CreatedOnUTC,
                CustomerId = Review.CustomerId,
                HelpfulnessNoTotal = Review.HelpfulnessNoTotal,
                HelpfulnessYesTotal = Review.HelpfulnessYesTotal,
                Id = Review.Id,
                IsApproved = Review.IsApproved,
                ProductId = Review.ProductId,
                Rating = Review.Rating,
                ReviewText = Review.ReviewText,
                Title = Review.Title,
                VendorId = Review.VendorId,    
                OrderId = Review.OrderId,
                CertifiedBuyerReview = Review.CertifiedBuyerReview,
                DisplayCertifiedBadge = Review.DisplayCertifiedBadge,
            };
             if (Product != null)
            {
                model.ProductName = Product.Name;
                model.ProductSeName = Product.GetSeName();
                model.ProductImageUrl = _pictureService.GetPictureUrl(Product.ProductPictures.FirstOrDefault().Picture);
                
            }
            if (Vendor != null)
            {
                model.VendorName = Vendor.Name;
                model.VendorSeName = Vendor.GetSeName();
            }
            return model;
        }

        public static VendorReviewListModel ToListModel(this VendorReview Review, IPictureService _pictureService, IProductService _productService, IVendorService _vendorService, ICustomerService _customerService)
        {
            var Product = _productService.GetProductById(Review.ProductId);
            var Vendor = _vendorService.GetVendorById(Review.VendorId);
            var Customer = _customerService.GetCustomerById(Review.CustomerId);
            var model = new VendorReviewListModel() {
                CreatedOnUTC = Review.CreatedOnUTC,
                CustomerId = Review.CustomerId,
                CustomerName = Customer.GetFullName(),
                HelpfulnessNoTotal = Review.HelpfulnessNoTotal,
                HelpfulnessYesTotal = Review.HelpfulnessYesTotal,
                Id = Review.Id,
                IsApproved = Review.IsApproved,
                ProductId = Review.ProductId,
                Rating = Review.Rating,
                ReviewText = Review.ReviewText,
                Title = Review.Title,
                VendorId = Review.VendorId,
                OrderId = Review.OrderId,
                CertifiedBuyerReview = Review.CertifiedBuyerReview,
                DisplayCertifiedBadge = Review.DisplayCertifiedBadge,     
                
            };
            if (Product != null)
            {
                model.ProductName = Product.Name;
                model.ProductSeName = Product.GetSeName();
                model.ProductImageUrl = _pictureService.GetPictureUrl(Product.ProductPictures.FirstOrDefault().Picture);
                
            }
            if (Vendor != null)
            {
                model.VendorName = Vendor.Name;
                model.VendorSeName = Vendor.GetSeName();
            }
            
            return model;
        }
        public static IList<VendorReviewListModel> ToListModel(this IList<VendorReview> VendorReviews, IPictureService _pictureService, IProductService _productService, IVendorService _vendorService, ICustomerService _customerService)
        {
            var vrList = new List<VendorReviewListModel>();
            foreach (var vr in VendorReviews)
                vrList.Add(vr.ToListModel(_pictureService, _productService, _vendorService, _customerService));

            return vrList;
        }

        public static VendorPayoutModel ToModel(this VendorPayout Payout, IOrderService _orderService)
        {
            var model = new VendorPayoutModel()
            {
                CommissionPercentage = Payout.CommissionPercentage,
                Id = Payout.Id,
                OrderId = Payout.OrderId,
                PayoutDate = Payout.PayoutDate,
                PayoutStatus = Payout.PayoutStatus,
                Remarks = Payout.Remarks,
                VendorId = Payout.VendorId,
                VendorOrderTotal = Payout.VendorOrderTotal,
                ShippingCharge = Payout.ShippingCharge,
                PayoutStatusName = Payout.PayoutStatus.ToString(),
            };
           
            if (Payout.PayoutStatus == PayoutStatus.Cancelled)
            {
                model.CommissionAmount = 0;
                model.PayoutAmount = 0;
                model.VendorOrderTotal = 0;
            }
            else if (_orderService != null)
            {
               var order =  _orderService.GetOrderById(Payout.OrderId);
               model.OrderDate = order.CreatedOnUtc;
/*
               var vendorItemTotalExShipping = Payout.VendorOrderTotal - model.ShippingCharge;
               var vendorItemTotalOriginal = (vendorItemTotalExShipping * 100) / (100 + Payout.CommissionPercentage);
               decimal commission = vendorItemTotalExShipping - vendorItemTotalOriginal;
               model.CommissionAmount = commission;
               model.PayoutAmount = vendorItemTotalOriginal;*/
                
               model.PayoutAmount = GetPayoutAmount(Payout.VendorOrderTotal,Payout.CommissionPercentage);
               model.CommissionAmount = model.VendorOrderTotal - model.PayoutAmount;
            }

            return model;
        }

        public static VendorPayoutListModel ToListModel(this VendorPayout Payout, IOrderService _orderService)
        {
            var model = new VendorPayoutListModel()
            {
                CommissionPercentage = Payout.CommissionPercentage,
                Id = Payout.Id,
                OrderId = Payout.OrderId,
                PayoutDate = Payout.PayoutDate,
                PayoutStatus = Payout.PayoutStatus,
                Remarks = Payout.Remarks,
                VendorId = Payout.VendorId,
                VendorOrderTotal = Payout.VendorOrderTotal,
                PayoutStatusName = Payout.PayoutStatus.ToString()
            };
            if (_orderService != null)
            {
                var order = _orderService.GetOrderById(Payout.OrderId);
                model.OrderId = order.Id;
            }
            return model;
        }

        public static string GetPictureUrl(int pictureId, ICacheManager _cacheManager, IPictureService _pictureService)
        {
            string cacheKey = string.Format("APEXOL-VENDOR-PICTURE-{0}", pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false);
                //little hack here. nulls aren't cacheable so set it to ""
                if (url == null)
                    url = "";

                return url;
            });
        }
        
        public static ExtendedVendorListModel ToListModel(this Domain.ExtendedVendor ExtendedVendor, IPictureService _pictureService, ICacheManager _cacheManager,ICountryService _countryService, IStateProvinceService _stateProvinceService)
        {
            var model = new ExtendedVendorListModel()
            {
                AddressLine1 = ExtendedVendor.AddressLine1,
                AddressLine2 = ExtendedVendor.AddressLine2,
                City = ExtendedVendor.City,
                CountryId = ExtendedVendor.CountryId,
                HelpfulnessEnabled = ExtendedVendor.HelpfulnessEnabled,
                LogoId = ExtendedVendor.LogoId,
                ReviewsEnabled = ExtendedVendor.ReviewsEnabled,
                StateProvinceId = ExtendedVendor.StateProvinceId,
                VendorId = ExtendedVendor.VendorId,
                TINNumber = ExtendedVendor.TinNumber,
                ServiceTaxNumber = ExtendedVendor.ServiceTaxNumber,
                ShortCode = ExtendedVendor.ShortCode,
                VatCST = ExtendedVendor.VatCST,
                Id = ExtendedVendor.Id,
                ZipCode = ExtendedVendor.ZipCode,
                PhoneNumber = ExtendedVendor.PhoneNumber,
                CommissionPercentage = ExtendedVendor.CommissionPercentage
            };
            var countries = _countryService.GetAllCountries();
            foreach (var country in countries)
            {
                var listItem = new SelectListItem
                {
                    Text = country.Name,
                    Value = country.Id.ToString()
                };
                if (country.Id == model.CountryId)
                    listItem.Selected = true;

                model.SelectedCountry.Add(listItem);

            }
            if (model.CountryId != 0)
            {
                var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId);
                foreach (var state in states)
                {
                    var listItem = new SelectListItem
                    {
                        Text = state.Name,
                        Value = state.Id.ToString()
                    };
                    if (state.Id == model.StateProvinceId)
                        listItem.Selected = true;
                    model.SelectedStateProvince.Add(listItem);
                }
            }

            return model;
        }

        public static ProductReviewListModel ToModel(this ProductReview ProductReview, IPictureService _pictureService)
        {
            var model = new ProductReviewListModel() {
                CustomerId = ProductReview.CustomerId,
                ProductId = ProductReview.ProductId,
                ProductName = ProductReview.Product.Name,
                Rating = ProductReview.Rating,
                ReviewText = ProductReview.ReviewText,
                ProductSeName = ProductReview.Product.GetSeName(),
                ProductImageUrl = _pictureService.GetPictureUrl(ProductReview.Product.ProductPictures.FirstOrDefault().Picture),
                Title = ProductReview.Title,
                CreatedOnUtc = ProductReview.CreatedOnUtc

            };
            return model;
        }

        public static IList<ProductReviewListModel> ToModel(this IList<ProductReview> ProductReviews, IPictureService _pictureService)
        {
            return ProductReviews.Select(pr => pr.ToModel(_pictureService)).ToList();
        }

     

    }
}
