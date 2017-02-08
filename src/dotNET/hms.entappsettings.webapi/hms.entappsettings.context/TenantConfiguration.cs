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

    // Tenant
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.28.0.0")]
    public partial class TenantConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Tenant>
    {
        public TenantConfiguration()
            : this("dbo")
        {
        }

        public TenantConfiguration(string schema)
        {
            ToTable("Tenant", schema);
            HasKey(x => x.TenantId);

            Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.TenantName).HasColumnName(@"TenantName").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(x => x.TenantCode).HasColumnName(@"TenantCode").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(30);
            Property(x => x.TenantDescription).HasColumnName(@"TenantDescription").HasColumnType("nvarchar").IsOptional().HasMaxLength(200);
            Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>