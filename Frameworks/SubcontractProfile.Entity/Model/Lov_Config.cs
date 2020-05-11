using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SubcontractProfile.Entity.Model
{
    public partial class Lov_Config
    {
        [Key]
        public int LOV_ID { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
        public string LOV_TYPE { get; set; }
        public string LOV_NAME { get; set; }
        public string DISPLAY_VAL { get; set; }
        public string LOV_VAL1 { get; set; }
        public string  LOV_VAL2 { get; set; }
        public string  LOV_VAL3 { get; set; }
        public string  LOV_VAL4 { get; set; }
        public string  LOV_VAL5 { get; set; }
        public string ACTIVEFLAG { get; set; }
        public int ORDER_BY { get; set; }
    }

}
