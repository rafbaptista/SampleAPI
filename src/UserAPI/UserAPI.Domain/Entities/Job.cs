using System;
using System.Collections.Generic;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Domain.Entities
{
    public class Job : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public bool Excluded { get; set; }
    }   
}
