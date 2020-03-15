using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DomainModel : IDomainModel
    {
        public DateTime CreateDateTime { get; set; }

        public DateTime LastUpdateDateTime { get; set; }

        public DomainModel()
        {
            CreateDateTime = DateTime.Now;
            LastUpdateDateTime = DateTime.Now;
        }
    }
}