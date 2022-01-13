using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
    public interface IEntity : IEntity<string>
    {
    }
}
