using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Data;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Domain;
using RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Services;
using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.Mvc;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_apexol_extended_vendor";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<ExtendedVendorService>().As<IExtendedVendorService>().InstancePerRequest();

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                //register named context
                builder.Register<IDbContext>(c => new ExtendedVendorObjectContext(dataProviderSettings.DataConnectionString))
                    .Named<IDbContext>(CONTEXT_NAME)
                    .InstancePerRequest();

                builder.Register<ExtendedVendorObjectContext>(c => new ExtendedVendorObjectContext(dataProviderSettings.DataConnectionString))
                    .InstancePerRequest();
            }
            else
            {
                //register named context
                builder.Register<IDbContext>(c => new ExtendedVendorObjectContext(c.Resolve<DataSettings>().DataConnectionString))
                    .Named<IDbContext>(CONTEXT_NAME)
                    .InstancePerRequest();

                builder.Register<ExtendedVendorObjectContext>(c => new ExtendedVendorObjectContext(c.Resolve<DataSettings>().DataConnectionString))
                    .InstancePerRequest();
            }

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Domain.ExtendedVendor>>()
                .As<IRepository<Domain.ExtendedVendor>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerRequest();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<VendorReview>>()
                .As<IRepository<Domain.VendorReview>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerRequest();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Domain.VendorPayout>>()
                .As<IRepository<Domain.VendorPayout>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerRequest();

        }


        public int Order
        {
            get { return 1; }
        }
    }
}
