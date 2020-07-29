using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileRaceModel
    {
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string RaceId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string RaceNameTh { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string RaceNameEn { get; set; }
    }
}
