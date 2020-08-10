using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileAddressModel
    {
        public System.Guid? AddressId { get; set; }

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

        public int? RegionId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string AddressTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string ZipCode { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Country { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string VillageName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(30)]
        public string RoomNo { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CompanyId { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string CreateBy { get; set; }

        public System.DateTime? ModifiedDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ModifiedBy { get; set; }




        public string address_type_name { get; set; }
        public string sub_district_name { get; set; }
        public string district_name { get; set; }
        public string province_name { get; set; }
        public string City { get; set; }
        public string outFullAddress { get; set; }
        public string location_code { get; set; }
    }
}
