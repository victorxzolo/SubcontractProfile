using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{
    public class SubcontractDropdown
    {

        /// =================================================================
        /// Author: AIS Fibre
        /// Description: PK class for the table [dbo].[SubcontractDropdown] 
        /// It's bit heavy (a lot of useless types in the DB) but you can
        /// use get by PKList even if your pk is a composite one...
        /// =================================================================
        public class SubcontractDropdown_PK
        {

            public System.Guid id { get; set; }

        }
        /// =================================================================
        /// Author: AIS Fibre -X10
        /// Description: Entity class for the table [dbo].[SubcontractDropdown] 
        /// =================================================================

        public class SubcontractProfileAddress : System.ICloneable
        {

            public System.Guid id { get; set; }

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
}
