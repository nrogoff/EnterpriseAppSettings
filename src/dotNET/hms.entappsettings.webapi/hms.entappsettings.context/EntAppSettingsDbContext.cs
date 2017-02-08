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
    public partial class EntAppSettingsDbContext : System.Data.Entity.DbContext, IEntAppSettingsDbContext
    {
        public System.Data.Entity.DbSet<AppSetting> AppSettings { get; set; } // AppSetting
        public System.Data.Entity.DbSet<AppSettingGroup> AppSettingGroups { get; set; } // AppSettingGroup
        public System.Data.Entity.DbSet<AppSettingSection> AppSettingSections { get; set; } // AppSettingSection
        public System.Data.Entity.DbSet<AppSettingType> AppSettingTypes { get; set; } // AppSettingType
        public System.Data.Entity.DbSet<Tenant> Tenants { get; set; } // Tenant

        static EntAppSettingsDbContext()
        {
            System.Data.Entity.Database.SetInitializer<EntAppSettingsDbContext>(null);
            EntAppSettingsDbContextStaticPartial(); // Create the following method in your partial class: private static void EntAppSettingsDbContextStaticPartial() { }
        }

        public EntAppSettingsDbContext()
            : base("Name=EntAppSettingsDb")
        {
            InitializePartial();
        }

        public EntAppSettingsDbContext(string connectionString)
            : base(connectionString)
        {
            InitializePartial();
        }

        public EntAppSettingsDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
            InitializePartial();
        }

        public EntAppSettingsDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            InitializePartial();
        }

        public EntAppSettingsDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            InitializePartial();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(new CodeFirstStoreFunctions.FunctionsConvention<EntAppSettingsDbContext>("dbo"));
            modelBuilder.ComplexType<GetChildAppSettingGroupsReturnModel>();
            modelBuilder.ComplexType<GetChildAppSettingSectionsReturnModel>();
            modelBuilder.ComplexType<GetParentAppSettingGroupsReturnModel>();
            modelBuilder.ComplexType<GetParentAppSettingSectionsReturnModel>();

            modelBuilder.Configurations.Add(new AppSettingConfiguration());
            modelBuilder.Configurations.Add(new AppSettingGroupConfiguration());
            modelBuilder.Configurations.Add(new AppSettingSectionConfiguration());
            modelBuilder.Configurations.Add(new AppSettingTypeConfiguration());
            modelBuilder.Configurations.Add(new TenantConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AppSettingConfiguration(schema));
            modelBuilder.Configurations.Add(new AppSettingGroupConfiguration(schema));
            modelBuilder.Configurations.Add(new AppSettingSectionConfiguration(schema));
            modelBuilder.Configurations.Add(new AppSettingTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new TenantConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(System.Data.Entity.DbModelBuilder modelBuilder);

        // Stored Procedures
        public int ClearDownAuditTables(int? keepMonths)
        {
            var keepMonthsParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@keep_months", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input, Value = keepMonths.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!keepMonths.HasValue)
                keepMonthsParam.Value = System.DBNull.Value;

            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@procResult", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };

            Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, "EXEC @procResult = [dbo].[ClearDownAuditTables] @keep_months", keepMonthsParam, procResultParam);

            return (int) procResultParam.Value;
        }

        // Table Valued Functions
        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetChildAppSettingGroups")]
        [CodeFirstStoreFunctions.DbFunctionDetails(DatabaseSchema = "dbo")]
        public IQueryable<GetChildAppSettingGroupsReturnModel> GetChildAppSettingGroups(int? appSettingGroupId)
        {
            var appSettingGroupIdParam = new System.Data.Entity.Core.Objects.ObjectParameter("AppSettingGroupId", typeof(int)) { Value = appSettingGroupId.GetValueOrDefault() };

            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.CreateQuery<GetChildAppSettingGroupsReturnModel>("[EntAppSettingsDbContext].[GetChildAppSettingGroups](@AppSettingGroupId)", appSettingGroupIdParam);
        }

        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetChildAppSettingSections")]
        [CodeFirstStoreFunctions.DbFunctionDetails(DatabaseSchema = "dbo")]
        public IQueryable<GetChildAppSettingSectionsReturnModel> GetChildAppSettingSections(int? appSettingSectionId)
        {
            var appSettingSectionIdParam = new System.Data.Entity.Core.Objects.ObjectParameter("AppSettingSectionId", typeof(int)) { Value = appSettingSectionId.GetValueOrDefault() };

            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.CreateQuery<GetChildAppSettingSectionsReturnModel>("[EntAppSettingsDbContext].[GetChildAppSettingSections](@AppSettingSectionId)", appSettingSectionIdParam);
        }

        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetParentAppSettingGroups")]
        [CodeFirstStoreFunctions.DbFunctionDetails(DatabaseSchema = "dbo")]
        public IQueryable<GetParentAppSettingGroupsReturnModel> GetParentAppSettingGroups(int? appSettingGroupId)
        {
            var appSettingGroupIdParam = new System.Data.Entity.Core.Objects.ObjectParameter("AppSettingGroupId", typeof(int)) { Value = appSettingGroupId.GetValueOrDefault() };

            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.CreateQuery<GetParentAppSettingGroupsReturnModel>("[EntAppSettingsDbContext].[GetParentAppSettingGroups](@AppSettingGroupId)", appSettingGroupIdParam);
        }

        [System.Data.Entity.DbFunction("EntAppSettingsDbContext", "GetParentAppSettingSections")]
        [CodeFirstStoreFunctions.DbFunctionDetails(DatabaseSchema = "dbo")]
        public IQueryable<GetParentAppSettingSectionsReturnModel> GetParentAppSettingSections(int? appSettingSectionId)
        {
            var appSettingSectionIdParam = new System.Data.Entity.Core.Objects.ObjectParameter("AppSettingSectionId", typeof(int)) { Value = appSettingSectionId.GetValueOrDefault() };

            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext.CreateQuery<GetParentAppSettingSectionsReturnModel>("[EntAppSettingsDbContext].[GetParentAppSettingSections](@AppSettingSectionId)", appSettingSectionIdParam);
        }

    }
}
// </auto-generated>
