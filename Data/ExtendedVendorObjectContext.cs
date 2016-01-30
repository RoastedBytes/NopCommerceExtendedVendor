using Nop.Core;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastedBytes.Nop.Plugin.Misc.ExtendedVendor.Data
{
    public class ExtendedVendorObjectContext : DbContext, IDbContext
    {
        public ExtendedVendorObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExtendedVendorMap());
            modelBuilder.Configurations.Add(new VendorPayoutMap());
            modelBuilder.Configurations.Add(new VendorReviewsMap());

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            Database.SetInitializer<ExtendedVendorObjectContext>(null);
            string script = CreateDatabaseInstallationScript();
            var sqls = script.Split(';');
            foreach (var sql in sqls)
            {
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    Database.ExecuteSqlCommand(sql);
                }
            }
            SaveChanges();
        }

        public void Uninstall()
        {
            //preserve the tables instead of deleting as they may contain good amount of data
            string now = DateTime.Now.Ticks.ToString();
            string format = "sp_rename '{0}','{1}'";
            var sql = string.Format(format, "ApexolExtendedVendor", "ApexolExtendedVendor_uninstalled_" + now);
            Database.ExecuteSqlCommand(sql);

            sql = string.Format(format, "ApexolVendorPayouts", "ApexolVendorPayouts_uninstalled_" + now);
            Database.ExecuteSqlCommand(sql);

            sql = string.Format(format, "ApexolVendorReviews", "ApexolVendorReviews_uninstalled_" + now);
            Database.ExecuteSqlCommand(sql);
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }
    }
}
