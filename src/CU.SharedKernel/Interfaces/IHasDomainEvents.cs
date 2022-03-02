using CU.SharedKernel.Base;
using System.Collections.Generic;

namespace CU.SharedKernel.Interfaces
{
    public interface IHasDomainEvents
    {
        List<DomainEventBase> DomainEvents { get; }
    }
}
