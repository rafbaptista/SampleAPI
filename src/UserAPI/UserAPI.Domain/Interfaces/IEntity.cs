using System;

namespace UserAPI.Domain.Interfaces
{
    public interface IEntity 
    {
        Guid Id { get; set; } 
        bool Excluded { get; set; }
    }
}
