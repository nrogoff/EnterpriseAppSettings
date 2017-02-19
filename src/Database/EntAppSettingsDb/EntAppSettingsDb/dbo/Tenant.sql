CREATE TABLE [dbo].[Tenant]
(
	[TenantId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TenantName] NVARCHAR(100) NOT NULL, 
    [TenantCode] VARCHAR(30) NULL, 
    [TenantDescription] NVARCHAR(200) NULL,
	[ModifiedDate] DATETIME        DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (suser_sname()) NOT NULL
)

GO

CREATE UNIQUE INDEX [UX_Tenant_TenantCode] ON [dbo].[Tenant] ([TenantCode])
