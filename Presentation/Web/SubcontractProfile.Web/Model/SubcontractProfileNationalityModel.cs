using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileNationalityModel
    {

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string NationalityId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string NationalityTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string NationalityEn { get; set; }

    }
}
