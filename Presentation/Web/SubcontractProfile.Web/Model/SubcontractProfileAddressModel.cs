using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileAddressModel
    {
        public System.Guid AddressId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string HouseNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Building { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string Floor { get; set; }

        public int? Moo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Soi { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string Road { get; set; }

        public int? SubDistrictId { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }


        public string address_type_id { get; set; }
        public string address_type_name { get; set; }
        public string sub_district_name { get; set; }
        public string district_name { get; set; }
        public string province_name { get; set; }
        public string village_name { get; set; }
        public string region_id { get; set; }
        public string room_no { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public string City { get; set; }
    }
}
