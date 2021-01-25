using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("suppliers")]
    public class Supplier
    {
        public int SupplierID { get; set; }
        [Required]
        [StringLength(15)]
        public string CompanyName { get; set; }
    }
}
