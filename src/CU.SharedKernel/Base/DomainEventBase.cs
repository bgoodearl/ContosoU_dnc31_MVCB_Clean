#if false
//TODO: Restore this when Domain Events are figured out
using MediatR;
using System;

namespace CU.SharedKernel.Base
{
    public abstract class DomainEventBase : INotification
    {
        public DomainEventBase()
        {
            WhenOccurred = DateTime.UtcNow;
        }

        public bool IsPublished { get; set; }
        public DateTime WhenOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
#endif