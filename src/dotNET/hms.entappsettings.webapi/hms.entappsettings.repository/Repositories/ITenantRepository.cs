using System;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    public interface ITenantRepository : IEditableRepository<Tenant>, IDisposable
    {
        void Add(Tenant tenant);
    }
}