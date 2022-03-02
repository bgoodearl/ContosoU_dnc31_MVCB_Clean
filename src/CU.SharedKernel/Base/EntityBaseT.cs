using System.Collections.Generic;

namespace CU.SharedKernel.Base
{
    public abstract class EntityBaseT<TId>
    {
        /// <summary>
        /// With a green-field project, Id would likely not be abstract here.
        /// Given the existing entity definitions in Contoso University with differently named ID fields, we make Id abstract here.
        /// </summary>
        public abstract TId Id { get; }

        public List<DomainEventBase> DomainEvents { get; private set; } = new List<DomainEventBase>();
    }
}