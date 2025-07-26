using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartPulseCaseStudy.Data
{
    public class TableContent
    {
        public DateTime date { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal totalPrice { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal totalQuantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal averagePrice { get; set; }
    }
}
