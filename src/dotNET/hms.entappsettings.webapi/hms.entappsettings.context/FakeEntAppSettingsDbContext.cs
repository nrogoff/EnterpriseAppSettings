// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.6
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace hms.entappsettings.context
{
    using hms.entappsettings.model;

    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.28.0.0")]
    public partial class FakeEntAppSettingsDbContext : IEntAppSettingsDbContext
    {
        public System.Data.Entity.DbSet<AppSetting> AppSettings { get; set; }
        public System.Data.Entity.DbSet<AppSettingGroup> AppSettingGroups { get; set; }
        public System.Data.Entity.DbSet<AppSettingSection> AppSettingSections { get; set; }
        public System.Data.Entity.DbSet<AppSettingType> AppSettingTypes { get; set; }
        public System.Data.Entity.DbSet<Tenant> Tenants { get; set; }

        public FakeEntAppSettingsDbContext()
        {
            AppSettings = new FakeDbSet<AppSetting>("AppSettingId");
            AppSettingGroups = new FakeDbSet<AppSettingGroup>("AppSettingGroupId");
            AppSettingSections = new FakeDbSet<AppSettingSection>("AppSettingSectionId");
            AppSettingTypes = new FakeDbSet<AppSettingType>("AppSettingTypeId");
            Tenants = new FakeDbSet<Tenant>("TenantId");

            InitializePartial();
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            ++SaveChangesCount;
            return System.Threading.Tasks.Task<int>.Factory.StartNew(() => 1);
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return System.Threading.Tasks.Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }

        partial void InitializePartial();

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public System.Data.Entity.Infrastructure.DbChangeTracker _changeTracker;
        public System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get { return _changeTracker; } }
        public System.Data.Entity.Infrastructure.DbContextConfiguration _configuration;
        public System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get { return _configuration; } }
        public System.Data.Entity.Database _database;
        public System.Data.Entity.Database Database { get { return _database; } }
        public System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity)
        {
            throw new System.NotImplementedException();
        }
        public System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors()
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.DbSet Set(System.Type entityType)
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }


        // Stored Procedures
        public int ClearDownAuditTables(int? keepMonths)
        {

            return 0;
        }

        // Table Valued Functions
        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetChildAppSettingGroups")]
        public IQueryable<GetChildAppSettingGroupsReturnModel> GetChildAppSettingGroups(int? appSettingGroupId)
        {
            return new System.Collections.Generic.List<GetChildAppSettingGroupsReturnModel>().AsQueryable();
        }

        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetChildAppSettingSections")]
        public IQueryable<GetChildAppSettingSectionsReturnModel> GetChildAppSettingSections(int? appSettingSectionId)
        {
            return new System.Collections.Generic.List<GetChildAppSettingSectionsReturnModel>().AsQueryable();
        }

        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetParentAppSettingGroups")]
        public IQueryable<GetParentAppSettingGroupsReturnModel> GetParentAppSettingGroups(int? appSettingGroupId)
        {
            return new System.Collections.Generic.List<GetParentAppSettingGroupsReturnModel>().AsQueryable();
        }

        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetParentAppSettingSections")]
        public IQueryable<GetParentAppSettingSectionsReturnModel> GetParentAppSettingSections(int? appSettingSectionId)
        {
            return new System.Collections.Generic.List<GetParentAppSettingSectionsReturnModel>().AsQueryable();
        }

    }
}
// </auto-generated>
