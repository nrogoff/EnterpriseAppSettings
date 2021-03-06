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


namespace hms.entappsettings.model
{

    // AppSettingSection
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.28.0.0")]
    public partial class AppSettingSection
    {
        public int AppSettingSectionId { get; set; } // AppSettingSectionId (Primary key)
        public int? ParentSectionId { get; set; } // ParentSectionId
        public string Section { get; set; } // Section (length: 50)
        public string Description { get; set; } // Description (length: 1000)
        public int Ordinality { get; set; } // Ordinality
        public System.DateTime ModifiedDate { get; set; } // ModifiedDate
        public string ModifiedBy { get; set; } // ModifiedBy (length: 50)

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<AppSetting> AppSettings { get; set; } // AppSetting.FK_AppSetting_AppSettingSection
        public virtual System.Collections.Generic.ICollection<AppSettingSection> AppSettingSections { get; set; } // AppSettingSection.FK_AppSettingSection_AppSettingSection

        // Foreign keys
        public virtual AppSettingSection ParentSection { get; set; } // FK_AppSettingSection_AppSettingSection

        public AppSettingSection()
        {
            Ordinality = 0;
            ModifiedDate = System.DateTime.UtcNow;
            ModifiedBy = "suser_sname()";
            AppSettings = new System.Collections.Generic.List<AppSetting>();
            AppSettingSections = new System.Collections.Generic.List<AppSettingSection>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
