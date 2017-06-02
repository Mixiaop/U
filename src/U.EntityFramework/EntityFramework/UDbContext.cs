using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using EntityFramework.DynamicFilters;
using U.Logging;
using U.Timing;
using U.Domain.Uow;
using U.Domain.Entities;
using U.Domain.Entities.Auditing;
using U.Events.Bus.Entities;
using U.EntityFramework.Mapping;

namespace U.EntityFramework
{
    /// <summary>
    /// DbContext 基类，应用下的 DbContext 可实现此类，继承 EF 的 DbContext
    /// </summary>
    public class UDbContext : DbContext, IDbContext
    {
        #region Fields & Ctor
        /// <summary>
        /// Used to trigger entity change events.
        /// </summary>
        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        /// <summary>
        /// Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to GUID generator.
        /// </summary>
        public IGuidGenerator GuidGenerator { get; set; }

         /// <summary>
        /// Constructor.
        /// Uses <see cref="IAbpStartupConfiguration.DefaultNameOrConnectionString"/> as connection string.
        /// </summary>
        protected UDbContext()
        {
            SetNullsForInjectedProperties();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            SetNullsForInjectedProperties();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UDbContext(DbCompiledModel model)
            : base(model)
        {
            SetNullsForInjectedProperties();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            SetNullsForInjectedProperties();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            SetNullsForInjectedProperties();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            SetNullsForInjectedProperties();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            SetNullsForInjectedProperties();
        }
        private void SetNullsForInjectedProperties()
        {
            Logger = NullLogger.Instance;
            EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            GuidGenerator = SequentialGuidGenerator.Instance;
        }
        #endregion

        public virtual void Initialize()
        {
            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //.Where(type => !String.IsNullOrEmpty(type.Namespace))
            //.Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
            //    type.BaseType.GetGenericTypeDefinition() == typeof(UEntityTypeConfiguration<>));
            //if (typesToRegister != null && typesToRegister.Count() > 0)
            //{
            //    foreach (var type in typesToRegister)
            //    {
            //        dynamic configurationInstance = Activator.CreateInstance(type);
            //        modelBuilder.Configurations.Add(configurationInstance);
            //    }
            //}

            base.OnModelCreating(modelBuilder);
            modelBuilder.Filter(UDataFilters.SoftDelete, (ISoftDelete d) => d.IsDeleted, false);
        }


        public override int SaveChanges()
        {
            try
            {
                ApplyUConcepts();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        public int NativeSaveChanges() {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                ApplyUConcepts();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        /// <summary>
        /// EF 提交之前处理 U 的一些自定义逻辑
        /// </summary>
        protected virtual void ApplyUConcepts()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        CheckAndSetId(entry);
                        SetCreationAuditProperties(entry);
                        EntityChangeEventHelper.TriggerEntityCreatingEvent(entry.Entity);
                        EntityChangeEventHelper.TriggerEntityCreatedEventOnUowCompleted(entry.Entity);
                        break;
                    case EntityState.Modified:
                        PreventSettingCreationAuditProperties(entry);
                        SetModificationAuditProperties(entry);

                        if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                        {
                            SetDeletionAuditProperties(entry);

                            EntityChangeEventHelper.TriggerEntityDeletingEvent(entry.Entity);
                            EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entry.Entity);
                        }
                        else
                        {
                            EntityChangeEventHelper.TriggerEntityUpdatingEvent(entry.Entity);
                            EntityChangeEventHelper.TriggerEntityUpdatedEventOnUowCompleted(entry.Entity);
                        }

                        break;
                    case EntityState.Deleted:
                        PreventSettingCreationAuditProperties(entry);
                        HandleSoftDelete(entry);
                        EntityChangeEventHelper.TriggerEntityDeletingEvent(entry.Entity);
                        EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entry.Entity);
                        break;
                }
            }
        }

        protected virtual void CheckAndSetId(DbEntityEntry entry)
        {
            if (entry.Entity is IEntity<Guid>)
            {
                var entity = entry.Entity as IEntity<Guid>;
                if (entity.IsTransient())
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }

        protected virtual void SetCreationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IHasCreationTime)
            {
                entry.Cast<IHasCreationTime>().Entity.CreationTime = Clock.Now;
            }
        }

        protected virtual void PreventSettingCreationAuditProperties(DbEntityEntry entry)
        {
            //TODO@Halil: Implement this when tested well (Issue #49)
            //if (entry.Entity is IHasCreationTime && entry.Cast<IHasCreationTime>().Property(e => e.CreationTime).IsModified)
            //{
            //    throw new DbEntityValidationException(string.Format("Can not change CreationTime on a modified entity {0}", entry.Entity.GetType().FullName));
            //}

            //if (entry.Entity is ICreationAudited && entry.Cast<ICreationAudited>().Property(e => e.CreatorUserId).IsModified)
            //{
            //    throw new DbEntityValidationException(string.Format("Can not change CreatorUserId on a modified entity {0}", entry.Entity.GetType().FullName));
            //}
        }

        protected virtual void SetModificationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IHasModificationTime)
            {
                entry.Cast<IHasModificationTime>().Entity.LastModificationTime = Clock.Now;
            }
        }

        protected virtual void HandleSoftDelete(DbEntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            var softDeleteEntry = entry.Cast<ISoftDelete>();

            softDeleteEntry.State = EntityState.Unchanged;
            softDeleteEntry.Entity.IsDeleted = true;

            SetDeletionAuditProperties(entry);
        }

        protected virtual void SetDeletionAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IHasDeletionTime)
            {
                entry.Cast<IHasDeletionTime>().Entity.DeletionTime = Clock.Now;
            }
        }

        private void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            Logger.Error("There are some validation errors while saving changes in EntityFramework:");
            foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            {
                Logger.Error(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            }
        }

        #region IDbContext
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : IEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public UDbContext Current
        {
            get {
                return this;
            }
        }

        public virtual bool ProxyCreationEnabled
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

        public virtual bool AutoDetectChangesEnabled
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
        #endregion
    }
}
