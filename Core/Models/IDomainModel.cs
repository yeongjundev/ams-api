using System;

namespace Core.Models
{
    public interface IDomainModel
    {
        public DateTime CreateDateTime { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}