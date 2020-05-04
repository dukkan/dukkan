using Abp.Domain.Services;

namespace Dukkan
{
    public abstract class DukkanDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected DukkanDomainServiceBase()
        {
            LocalizationSourceName = DukkanConsts.LocalizationSourceName;
        }
    }
}
