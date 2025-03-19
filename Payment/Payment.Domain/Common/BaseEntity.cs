using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        [Key]
        [Column("id")]  
        public virtual T Id { get; set; }
    }
}
