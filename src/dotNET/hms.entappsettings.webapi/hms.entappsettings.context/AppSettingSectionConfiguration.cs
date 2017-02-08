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

    // AppSettingSection
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.28.0.0")]
    public partial class AppSettingSectionConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AppSettingSection>
    {
        public AppSettingSectionConfiguration()
            : this("dbo")
        {
        }

        public AppSettingSectionConfiguration(string schema)
        {
            ToTable("AppSettingSection", schema);
            HasKey(x => x.AppSettingSectionId);

            Property(x => x.AppSettingSectionId).HasColumnName(@"AppSettingSectionId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.ParentSectionId).HasColumnName(@"ParentSectionId").HasColumnType("int").IsOptional();
            Property(x => x.Section).HasColumnName(@"Section").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsOptional().HasMaxLength(1000);
            Property(x => x.Ordinality).HasColumnName(@"Ordinality").HasColumnType("int").IsRequired();
            Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.ParentSection).WithMany(b => b.AppSettingSections).HasForeignKey(c => c.ParentSectionId).WillCascadeOnDelete(false); // FK_AppSettingSection_AppSettingSection
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>