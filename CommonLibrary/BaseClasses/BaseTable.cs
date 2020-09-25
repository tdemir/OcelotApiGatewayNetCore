using System;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.BaseClasses
{
    public abstract class BaseTable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}