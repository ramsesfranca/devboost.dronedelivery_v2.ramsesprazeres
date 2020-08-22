using System;

namespace DroneDelivery.Domain.Core
{
    public abstract class EntidadeBase<T> : IEntidadeBase<T> where T : IEquatable<T>

    {
        public T Id { get; protected set; }
    }
}
