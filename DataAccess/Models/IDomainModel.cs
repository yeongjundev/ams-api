using System;

namespace DataAccess.Models
{
    public interface IDomainModel
    {
        public DateTime CreateDateTime { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}