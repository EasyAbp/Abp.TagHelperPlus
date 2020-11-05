using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.Abp.TagHelperPlus.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; set; }
        
        public virtual Guid UserId { get; set; }

        protected Book() {}
        
        public Book(
            Guid id,
            string name,
            Guid userId) : base(id)
        {
            Name = name;
            UserId = userId;
        }
    }
}