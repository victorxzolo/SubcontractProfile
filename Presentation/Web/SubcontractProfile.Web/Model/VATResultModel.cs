using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class VATResultModel
    {
        public VATResultModel()
        {
            Title = "";
            Message = "";
            StatusCode = "400";
            Value = null;
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public VATModal Value { get; set; }
    }
    public class VATModal
    {
        public string vNID { get; set; }
        public string vtin { get; set; }
        public string vtitleName { get; set; }
        public string vName { get; set; }
        public string vSurname { get; set; }
        public string vBranchTitleName { get; set; }
        public string vBranchName { get; set; }
        public string vBranchNumber { get; set; }
        public string vBuildingName { get; set; }
        public string vFloorNumber { get; set; }
        public string vVillageName { get; set; }
        public string vRoomNumber { get; set; }
        public string vHouseNumber { get; set; }
        public string vMooNumber { get; set; }
        public string vSoiName { get; set; }
        public string vStreetName { get; set; }
        public string vThambol { get; set; }
        public string vAmphur { get; set; }
        public string vProvince { get; set; }
        public string vPostCode { get; set; }
        public string vBusinessFirstDate { get; set; }
        public string vmsgerr { get; set; }
        public string outConcataddr { get; set; }
    }

    public class SearchVATModel: DataTableAjaxModel
    {
        public string tIN { get; set; }
        public int page_index { get; set; }
        public int page_size { get; set; }

        public string sort_col { get; set; }
        public string sort_dir { get; set; }
    }

}
