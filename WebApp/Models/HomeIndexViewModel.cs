using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class HomeIndexViewModel
    {
        public int VisitorCount;
        public ICollection<Product> Products { get; set; }
    }
}
