﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileLocationRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileLocationRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> GetByLocationId(System.Guid locationId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation subcontractProfileLocation);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> subcontractProfileLocationList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation subcontractProfileLocation);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation_PK> pkList);
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> SearchLocation(System.Guid company_id,string location_code,
            string location_name, string location_name_en, string phone);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocationList>> SearchListLocation(SubcontractProfile.WebApi.Services.Model.SearchSubcontractProfileLocationQuery data);
    }
}
