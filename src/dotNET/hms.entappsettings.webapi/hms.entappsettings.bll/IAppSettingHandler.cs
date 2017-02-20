using System;
using System.Collections.Generic;
using hms.entappsettings.contracts;

namespace hms.entappsettings.bll
{
    /// <summary>
    /// App Setting Handler
    /// </summary>
    public interface IAppSettingHandler : IDisposable
    {
        /// <summary>
        /// Returns the resultant AppSettings for a particular affiliate, including unaffiliated (=0) and including all Groups that apply.
        /// Also includes all settings that have been overridden. These are flagged as overridden.
        /// </summary>
        /// <param name="tenantId">The Id of the tenant. When used from management UI then can leave as null to get all Tenants</param>
        /// <param name="appSettingGroupId"></param>
        /// <param name="includeInternal">Include internal App Settings. Default = false</param>
        /// <returns>Collection of AppSettingsWithOverride.</returns>
        IEnumerable<AppSettingWithOverrideDTO> GetAppSettingWithOverrideDtos(int tenantId, int appSettingGroupId, bool includeInternal = false);
    }
}