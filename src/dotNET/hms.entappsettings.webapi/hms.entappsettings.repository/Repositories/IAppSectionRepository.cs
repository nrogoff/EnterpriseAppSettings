using System;
using System.Linq;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    public interface IAppSettingSectionRepository : IEditableRepository<AppSettingSection>, IDisposable
    {
        IQueryable<GetParentAppSettingSectionsReturnModel> GetAppSettingSectionParents(int appSectionSectionId);
        void Add(AppSettingSection appSettingSection);
    }
}