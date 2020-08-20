using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractDropdownModel
    {
        public int? id { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string dropdown_name { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string dropdown_value { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string dropdown_text { get; set; }

        public int? order_no { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string is_active { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
