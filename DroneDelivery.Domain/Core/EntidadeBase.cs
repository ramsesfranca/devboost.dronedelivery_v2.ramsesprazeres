using System;
using System.ComponentModel.DataAnnotations;

namespace DroneDelivery.Domain.Core
{
    public abstract class EntidadeBase<T> : IEntidadeBase<T> where T : IEquatable<T>

    {
        [Key]
        public T Id { get; protected set; }
    }
}
