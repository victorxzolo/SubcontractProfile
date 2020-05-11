using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SubcontractProfile.Models
{
   public partial class Product
    {
        [Key]
        public int prodId { get; set; }
        public string prodCode { get; set; }
        public string prodName { get; set; }
        //public int prodCost { get; set; }
    }
}
