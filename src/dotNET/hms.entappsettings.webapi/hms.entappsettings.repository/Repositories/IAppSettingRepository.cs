using System;
using System.Collections.Generic;
using System.Linq;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    /// <summary>
    /// Application Settings Repository
    /// </summary>
    public interface IAppSettingRepository : IEditableRepository<AppSetting>, IDisposable
    {

        IQueryable<AppSetting> GetAllAppSettings(bool includeInternal = false);

        IEnumerable<AppSetting> GetResultantAppSettings(int tenantId, int appSettingGroupId, bool includeInternal = false);

        void Add(AppSetting appSetting);
    }
}