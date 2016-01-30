using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Controllers;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Models;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Services;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Controllers
{
    public class ExtendedVendorController : BasePublicController
    {
        private readonly IExtendedVendorService _extendedVendorService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IVendorService _vendorService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly ICustomerService _customerService;

        public ExtendedVendorController(IExtendedVendorService extendedVendorService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IVendorService vendorService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IOrderService orderService,
            IProductService productService,
            CatalogSettings catalogSettings,
            IWorkflowMessageService workflowMessageService,
            ICustomerActivityService customerActivityService,
            LocalizationSettings localizationSettings,
            ISettingService settingService,
            IStoreService storeService,
            ICustomerService customerService)
        {
            this._extendedVendorService = extendedVendorService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._vendorService = vendorService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._orderService = orderService;
            this._productService = productService;
            this._catalogSettings = catalogSettings;
            this._workflowMessageService = workflowMessageService;
            this._customerActivityService = customerActivityService;
            this._localizationSettings = localizationSettings;
            this._settingService = settingService;
            this._storeService = storeService;
            this._customerService = customerService;
        }


        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var settings = _settingService.LoadSetting<ExtendedVendorSettings>(storeScope);

            var model = new ConfigurationModel {
                DefaultCommissionPercentage = settings.DefaultCommissionPercentage,
                DefaultShippingCharge = settings.DefaultShippingCharge,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.DefaultCommissionPercentage_OverrideForStore = _settingService.SettingExists(settings, x => x.DefaultCommissionPercentage, storeScope);
                model.DefaultShippingCharge_OverrideForStore = _settingService.SettingExists(settings, x => x.DefaultShippingCharge, storeScope);
            }

            return View("ExtendedVendor/Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var settings = _settingService.LoadSetting<ExtendedVendorSettings>(storeScope);

            //save settings
            settings.DefaultShippingCharge = model.DefaultShippingCharge;
            settings.DefaultCommissionPercentage = model.DefaultCommissionPercentage;


            if (model.DefaultCommissionPercentage_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(settings, x => x.DefaultCommissionPercentage, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(settings, x => x.DefaultCommissionPercentage, storeScope);

            if (model.DefaultShippingCharge_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(settings, x => x.DefaultShippingCharge, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(settings, x => x.DefaultShippingCharge, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            return Configure();
        }

        [HttpPost]
        public ActionResult ListReviewsAdmin(int VendorId, DataSourceRequest command)
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");

            var records = _extendedVendorService.GetVendorReviews(VendorId, null, null, command.Page, command.PageSize);
            var dataModel = records.Select(x =>
            {
                var order = _orderService.GetOrderById(x.OrderId);
                var orderItems = order.OrderItems.First(oi => oi.Product.Id == x.ProductId);
                return x.ToModel(order, orderItems.Product);

            });

            var model = new DataSourceResult {
                Data = dataModel,
                Total = records.Count
            };

            return new JsonResult {
                Data = model
            };
        }
        [HttpPost]
        public ActionResult ListPayoutsAdmin(int VendorId, DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");

            var records = _extendedVendorService.GetVendorPayouts(VendorId, null, command.Page, command.PageSize);
            CultureInfo cInf = new CultureInfo(_workContext.WorkingCurrency.DisplayLocale);
            RegionInfo rInf = new RegionInfo(cInf.LCID);
            var dataModel = records
                .Select(x =>
                {
                    var m = x.ToModel(_orderService);
                    m.CurrencySymbol = rInf.CurrencySymbol;
                    return m;
                })
                .ToList();
            var model = new DataSourceResult {
                Data = dataModel,
                Total = records.Count,
                ExtraData = new {
                    VendorTotal = rInf.CurrencySymbol + " " + dataModel.Sum(m => m.VendorOrderTotal).ToString("0.00"),
                    CommissionTotal =
                        rInf.CurrencySymbol + " " + dataModel.Sum(m => m.CommissionAmount).ToString("0.00"),
                    PayoutTotal = rInf.CurrencySymbol + " " + dataModel.Sum(m => m.PayoutAmount).ToString("0.00")
                }
            };
            return new JsonResult {
                Data = model
            };
        }

        [HttpPost]
        public ActionResult DeletePayout(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");

            var payout = _extendedVendorService.GetVendorPayout(Id);
            if (payout != null)
                _extendedVendorService.DeleteVendorPayout(payout);

            return new NullJsonResult();

        }

        [HttpPost]
        public ActionResult UpdatePayout(VendorPayoutModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");


            var payout = _extendedVendorService.GetVendorPayout(model.Id);
            if (payout != null)
            {
                payout.CommissionPercentage = model.CommissionPercentage;
                payout.PayoutDate = model.PayoutDate;
                payout.PayoutStatus = model.PayoutStatus;
                payout.Remarks = model.Remarks;
                payout.VendorOrderTotal = model.VendorOrderTotal;
                payout.ShippingCharge = model.ShippingCharge;
                _extendedVendorService.SaveVendorPayout(payout);
            }
            return new NullJsonResult();
        }

        [NonAction]
        public int GetCurrentVendorId()
        {

            if (ControllerContext == null)
            {
                var context = new ControllerContext(System.Web.HttpContext.Current.Request.RequestContext, this);
                ControllerContext = context;
            }
            var vendorId = Convert.ToInt32(ControllerContext.RequestContext.RouteData.Values["id"]);
            return vendorId;
        }


        [AdminAuthorize]
        public ActionResult EditVendor(int Id = 0)
        {

            var vendor = _extendedVendorService.GetExtendedVendor(Id);
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var settings = _settingService.LoadSetting<ExtendedVendorSettings>(storeScope);

            if (vendor == null)
            {
                //first time extended vendor infor
                vendor = new Domain.ExtendedVendor {
                    VendorId = Id,
                    CommissionPercentage = settings.DefaultCommissionPercentage,
                    ShippingCharge = settings.DefaultShippingCharge
                };
            }
            var model = vendor.ToListModel(_pictureService, _cacheManager, _countryService, _stateProvinceService);
            return View("ExtendedVendor/EditVendor", model);
        }

        [HttpPost]
        public ActionResult SaveVendor(ExtendedVendorModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");

            var q = _extendedVendorService.GetExtendedVendor(model.VendorId) ?? new Domain.ExtendedVendor();

            q.AddressLine1 = model.AddressLine1;
            q.AddressLine2 = model.AddressLine2;
            q.City = model.City;
            q.CountryId = model.CountryId;
            q.HelpfulnessEnabled = model.HelpfulnessEnabled;
            q.VendorId = model.VendorId;
            q.PhoneNumber = model.PhoneNumber;
            q.ReviewsEnabled = model.ReviewsEnabled;
            q.ServiceTaxNumber = model.ServiceTaxNumber;
            q.ShortCode = model.ShortCode;
            q.StateProvinceId = model.StateProvinceId;
            q.TinNumber = model.TinNumber;
            q.VatCST = model.VatCST;
            q.ZipCode = model.ZipCode;
            q.CommissionPercentage = model.CommissionPercentage;
            q.ShippingCharge = model.ShippingCharge;
            _extendedVendorService.SaveExtendedVendor(q);

            return new NullJsonResult();
        }
        [HttpPost]
        [Authorize]
        public ActionResult SaveReview(VendorReviewModel model)
        {
            //only requested review
            var custId = _workContext.CurrentCustomer.Id;
            if (!_extendedVendorService.IsVendorReviewDone(model.VendorId, custId, model.OrderId, model.ProductId))
            {
                //check if customer can review this vendor for this order
                if (_extendedVendorService.CanCustomerReviewVendor(model.VendorId, custId, model.OrderId))
                {
                    //yes he can
                    var review = new VendorReview {
                        CreatedOnUTC = DateTime.Now,
                        CustomerId = custId,
                        HelpfulnessNoTotal = 0,
                        HelpfulnessYesTotal = 0,
                        IsApproved = false,
                        OrderId = model.OrderId,
                        ProductId = model.ProductId,
                        Rating = model.Rating,
                        ReviewText = model.ReviewText,
                        Title = model.Title,
                        VendorId = model.VendorId
                    };

                    _extendedVendorService.SaveVendorReview(review);

                }
            }
            else
            {
                return Json(new {
                    success = false,
                    error = "You have already reviewed this order"
                });
            }
            return Json(new {
                success = false,
                error = "You need to login to review a vendor"
            });
        }

        [HttpPost]
        public ActionResult DeleteReview(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");


            var review = _extendedVendorService.GetVendorReview(Id);
            if (review != null)
                _extendedVendorService.DeleteVendorReview(review);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult UpdateReview(VendorReviewModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return Content("Access denied");


            var review = _extendedVendorService.GetVendorReview(model.Id);

            if (review == null)
                return new NullJsonResult();

            review.IsApproved = model.IsApproved;
            review.Rating = model.Rating;
            review.ReviewText = model.ReviewText;
            review.Title = model.Title;
            review.HelpfulnessNoTotal = model.HelpfulnessNoTotal;
            review.HelpfulnessYesTotal = model.HelpfulnessYesTotal;
            _extendedVendorService.SaveVendorReview(review);
            return new NullJsonResult();
        }
        [ChildActionOnly]
        public ActionResult DisplayNavigation()
        {
            return View("ExtendedVendor/DisplayNavigation");
        }
        [ChildActionOnly]
        public ActionResult CertifiedBadgeHandler(object additionalData)
        {
            int intId = Convert.ToInt32(additionalData);
            if (intId == 0)
                return null;
            //check if the customer actually purchased this item from this store
            var productReview = _productService.GetProductReviewById(intId);
            if (productReview == null)
                return null;

            var cOrderItems = _orderService.GetAllOrderItems(null, productReview.CustomerId, null, null, OrderStatus.Complete, null, null);

            return cOrderItems.Any(x => x.ProductId == productReview.ProductId) ? View("ExtendedVendor/CertifiedBadge") : null;
        }
        [ChildActionOnly]
        public ActionResult VendorRatingHandler(object additionalData)
        {


            int intId = Convert.ToInt32(additionalData);
            if (intId == 0)
                return null;
            var vendor = _vendorService.GetVendorById(intId);

            var allReviews = _extendedVendorService.GetVendorReviews(intId, null, true);
            var averageRating = allReviews.Any() ? allReviews.Average(x => x.Rating) : 0;
            var model = new VendorRatingModel() {
                AverageRating = (decimal)averageRating,
                TotalRatings = allReviews.Count,
                VendorId = intId,
                VendorSeName = vendor.GetSeName(_workContext.WorkingLanguage.Id)
            };
            return View("ExtendedVendor/VendorRating", model);
        }

        [Authorize]
        public ActionResult UserReviews(bool Redirection = false)
        {
            var customer = _workContext.CurrentCustomer;

            var productReviews = _productService.GetAllProductReviews(customer.Id, null);
            var vendorReviews = _extendedVendorService.GetVendorReviews(null, customer.Id, null);


            var model = new CustomerReviewSummaryModel() {
                TotalProductReviews = productReviews.Count,
                TotalVendorReviews = vendorReviews.Count,
                IsRedirection = Redirection
            };
            if (productReviews.Count > 0)
            {
                for (var index = 0; index < 5; index++)
                {
                    model.ProductRatingCounts[index] = productReviews.Count(pr => pr.Rating == (5 - index));
                }
            }

            if (vendorReviews.Count > 0)
            {
                for (var index = 0; index < 5; index++)
                {
                    model.VendorRatingCounts[index] = vendorReviews.Count(pr => pr.Rating == (5 - index));
                }
            }
            if (productReviews.Count > 0)
            {
                var recentProductReview = productReviews.OrderByDescending(pr => pr.CreatedOnUtc).First();
                model.LastRatedProductReview = recentProductReview;
            }
            if (vendorReviews.Count > 0)
            {
                var recentVendorReview = vendorReviews.OrderByDescending(pr => pr.CreatedOnUTC).First();
                model.LastRatedVendorReview = recentVendorReview;
            }
            model.CustomerName = customer.FormatUserName();
            return View("ExtendedVendor/UserReviews", model);
        }

        [Authorize]
        public ActionResult UserReviewsData(string Type)
        {
            Type = Type.ToLower();
            var customer = _workContext.CurrentCustomer;
            if (customer != null && customer.IsRegistered())
            {
                if (Type == "product")
                {
                    var productReviews = _productService.GetAllProductReviews(customer.Id, null);
                    productReviews = productReviews.OrderByDescending(pr => pr.CreatedOnUtc).ToList();
                    var model = new PublicProductReviewDisplayModel() {
                        ProductReviews = productReviews.ToModel(_pictureService)
                    };

                    foreach (var pr in productReviews)
                    {
                        if (model.ProductImageUrl.ContainsKey(pr.ProductId))
                            continue;
                        var imageUrl = _pictureService.GetPictureUrl(pr.Product.ProductPictures.FirstOrDefault().Picture);
                        model.ProductImageUrl.Add(pr.ProductId, imageUrl);
                    }
                    return View("ExtendedVendor/UserProductReviews", model);
                }
                if (Type == "vendor")
                {
                    var vendorReviews = _extendedVendorService.GetVendorReviews(null, customer.Id, null, 1, int.MaxValue).ToList();


                    var model = new PublicVendorReviewDisplayModel() {
                        VendorReviews = vendorReviews.ToListModel(_pictureService, _productService, _vendorService, _customerService)
                    };
                    return View("ExtendedVendor/UserVendorReviews", model);
                }
                if (Type == "pending")
                {
                    var customerOrders = _orderService.SearchOrders(0, 0, customer.Id, 0, 0, 0, 0, null, null, null, OrderStatus.Complete, PaymentStatus.Paid, ShippingStatus.Delivered).ToList();
                    var pendingReviewProducts = _extendedVendorService.GetProductsWithPendingReviews(customerOrders, customer.Id);

                    var model = new PublicPendingReviewDisplayModel();
                    var vendorList = new Dictionary<int, Vendor>(); //storing vendors for performance

                    foreach (var prp in pendingReviewProducts)
                    {

                        var order = prp.Key;
                        var orderModel = new PendingOrderModel() {
                            OrderId = order.Id,
                        };
                        var reviewModelList = new List<PendingReviewModel>();

                        foreach (var product in prp.Value)
                        {
                            Vendor v = null;
                            if (vendorList.ContainsKey(product.VendorId))
                                v = vendorList[product.VendorId];
                            else
                            {
                                v = _vendorService.GetVendorById(product.VendorId);
                                vendorList.Add(v.Id, v);//add it to a dictionary for avoiding next time database query for same vendor
                            }

                            var prModel = new PendingReviewModel() {
                                OrderId = order.Id,
                                ProductId = product.Id,
                                ProductName = product.Name,
                                ProductImageUrl = _pictureService.GetPictureUrl(product.ProductPictures.FirstOrDefault().Picture),
                                ProductSeName = product.GetSeName(),
                                VendorName = v.Name,
                                VendorSeName = v.GetSeName()
                            };
                            reviewModelList.Add(prModel);
                        }
                        model.PendingReviews.Add(orderModel, reviewModelList);
                    }
                    return View("ExtendedVendor/UserPendingReviews", model);
                }
                return InvokeHttp404();
            }
            return RedirectToRoute("Homepage");
        }

        [Authorize]
        public ActionResult EditExtendedReview(int OrderId, int ProductId)
        {
            var product = _productService.GetProductById(ProductId);
            var customer = _workContext.CurrentCustomer;
            var customerProductReviews = product.ProductReviews.Where(m => m.CustomerId == customer.Id).OrderByDescending(m => m.CreatedOnUtc);
            var model = new PublicReviewEditModel();
            if (!customerProductReviews.Any())
            {
                model.AddProductReviewModel = new AddProductReviewModel();
            }
            else
            {
                var pr = customerProductReviews.First();
                model.AddProductReviewModel = new AddProductReviewModel() {

                    Title = pr.Title,
                    ReviewText = pr.ReviewText,
                    Rating = pr.Rating,

                };
            }

            var customerVendorReview = _extendedVendorService.GetVendorReview(product.VendorId, customer.Id, OrderId, product.Id);
            var vendor = _vendorService.GetVendorById(product.VendorId);

            if (customerVendorReview == null)
            {
                model.VendorReviewListModel = new VendorReviewListModel();
            }
            else
            {
                var order = _orderService.GetOrderById(OrderId);
                model.VendorReviewListModel = customerVendorReview.ToListModel(_pictureService, product, vendor, order);
            }

            model.VendorReviewListModel.CustomerName = customer.FormatUserName();

            var extendedVendor = _extendedVendorService.GetExtendedVendor(product.VendorId);

            model.VendorName = vendor.Name;
            model.ProductName = product.Name;
            model.ProductSeName = product.GetSeName();
            model.VendorSeName = vendor.GetSeName();

            if (product.ProductPictures.Count > 0)
                model.ProductImageUrl = _pictureService.GetPictureUrl(product.ProductPictures.First().Picture);

            if (extendedVendor != null && extendedVendor.LogoId > 0)
            {
                model.VendorImageUrl = _pictureService.GetPictureUrl(extendedVendor.LogoId);
            }

            model.VendorReviewListModel.ProductId = product.Id;
            model.VendorReviewListModel.OrderId = OrderId;
            return View("ExtendedVendor/EditExtendedReview", model);
        }

        [HttpPost, ActionName("EditExtendedReview")]
        public ActionResult EditExtendedReviewPost(int OrderId, int ProductId, PublicReviewEditModel model)
        {

            var ProductReview = model.AddProductReviewModel;
            var VendorReview = model.VendorReviewListModel;
            var customer = _workContext.CurrentCustomer;
            var product = _productService.GetProductById(ProductId);
            if (product == null || product.Deleted || !product.Published || !product.AllowCustomerReviews)
                return RedirectToRoute("HomePage");
            if (ModelState.IsValid)
            {
                var canCustomerReviewVendor = _extendedVendorService.CanCustomerReviewVendor(product.VendorId, customer.Id, OrderId);
                if (!canCustomerReviewVendor)
                {
                    return InvokeHttp404();
                }

                var customerProductReviews = product.ProductReviews.Where(m => m.CustomerId == customer.Id).OrderByDescending(m => m.CreatedOnUtc);
                var rating = ProductReview.Rating;
                if (rating < 1 || rating > 5)
                    rating = _catalogSettings.DefaultProductRatingValue;
                var isApproved = !_catalogSettings.ProductReviewsMustBeApproved;
                ProductReview productReview;
                if (customerProductReviews.Any())
                {
                    productReview = customerProductReviews.First();
                    productReview.Title = ProductReview.Title;
                    productReview.ReviewText = ProductReview.ReviewText;
                    productReview.Rating = rating;
                    productReview.IsApproved = isApproved;
                    //updating
                }
                else
                {
                    productReview = new ProductReview() {
                        ProductId = product.Id,
                        CustomerId = _workContext.CurrentCustomer.Id,
                        Title = ProductReview.Title,
                        ReviewText = ProductReview.ReviewText,
                        Rating = rating,
                        HelpfulYesTotal = 0,
                        HelpfulNoTotal = 0,
                        IsApproved = isApproved,
                        CreatedOnUtc = DateTime.UtcNow,
                    };
                    product.ProductReviews.Add(productReview);
                }
                _productService.UpdateProduct(product);

                //update product totals
                _productService.UpdateProductReviewTotals(product);

                //notify store owner
                if (_catalogSettings.NotifyStoreOwnerAboutNewProductReviews)
                    _workflowMessageService.SendProductReviewNotificationMessage(productReview, _localizationSettings.DefaultAdminLanguageId);

                //activity log
                _customerActivityService.InsertActivity("PublicStore.AddProductReview", _localizationService.GetResource("ActivityLog.PublicStore.AddProductReview"), product.Name);


                rating = VendorReview.Rating;
                if (rating < 1 || rating > 5)
                    rating = _catalogSettings.DefaultProductRatingValue;


                var vendorReview = _extendedVendorService.GetVendorReview(product.VendorId, customer.Id, OrderId, ProductId);
                if (vendorReview == null)
                {
                    vendorReview = new VendorReview() {
                        IsApproved = isApproved,
                        CertifiedBuyerReview = true,
                        DisplayCertifiedBadge = true,
                        CreatedOnUTC = DateTime.Now,
                        CustomerId = customer.Id,
                        HelpfulnessNoTotal = 0,
                        HelpfulnessYesTotal = 0,
                        OrderId = OrderId,
                        ProductId = product.Id,
                        Rating = rating,
                        ReviewText = VendorReview.ReviewText,
                        Title = VendorReview.Title,
                        VendorId = product.VendorId
                    };
                }
                else
                {
                    vendorReview.IsApproved = isApproved;
                    vendorReview.ReviewText = VendorReview.ReviewText;
                    vendorReview.Title = VendorReview.Title;
                    vendorReview.Rating = rating;
                    vendorReview.HelpfulnessYesTotal = 0;
                    vendorReview.HelpfulnessNoTotal = 0;
                }
                _extendedVendorService.SaveVendorReview(vendorReview);
                return RedirectToRoute("ExtendedVendorReviewCenterLoader", new { Redirection = true });
            }
            else
            {
                return RedirectToRoute("HomePage");
            }
        }

        public ActionResult ExtendedVendorDetails(string SeName)
        {
            var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
            var record = urlRecordService.GetBySlug(SeName);
            int vendorId = 0;
            if (record != null)
            {
                if (record.IsActive && record.EntityName.ToLowerInvariant() == "vendor")
                {
                    vendorId = record.EntityId;
                }
            }
            if (vendorId == 0)
                return InvokeHttp404();

            var vendor = _vendorService.GetVendorById(vendorId);
            var extendedVendor = _extendedVendorService.GetExtendedVendor(vendorId);

            var vendorReviews = _extendedVendorService.GetVendorReviews(vendorId, null, null, 1, int.MaxValue).ToList();

            var model = new VendorReviewSummaryModel()
            {
                VendorReviewDisplayModel = new PublicVendorReviewDisplayModel()
                {
                    VendorReviews =
                        vendorReviews.ToListModel(_pictureService, _productService, _vendorService, _customerService)
                },
                VendorName = vendor.Name,
                TotalVendorReviews = vendorReviews.Count,
            };
            for (var index = 0; index < 5; index++)
            {
                model.VendorRatingCounts[index] = vendorReviews.Count(pr => pr.Rating == (5 - index));
            }

            return View("ExtendedVendor/VendorDetails", model);

        }
    }
}
