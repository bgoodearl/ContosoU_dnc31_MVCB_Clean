using CU.SharedKernel.Base;
using System.Collections.Generic;

namespace CU.SharedKernel.Interfaces
{
    public interface IHasDomainEvent
    {
        List<DomainEventBase> DomainEvents { get; set; }
    }
}
