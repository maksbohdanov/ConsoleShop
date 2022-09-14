using System;

namespace Store.DAL.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N")[..6];
        }
    }
}
