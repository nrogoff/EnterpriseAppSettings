using System;
using System.Collections.Generic;
using System.Linq;
using hms.entappsettings.contracts;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    /// <summary>
    /// App Setting Groups Repository
    /// </summary>
    public interface IAppSettingGroupRepository : IEditableRepository<AppSettingGroup>, IDisposable
    {
        IQueryable<GetParentAppSettingGroupsReturnModel> GetAppSettingGroupParents(int appSettingGroupId);


        void Add(AppSettingGroup appSettingGroup);
    }
}