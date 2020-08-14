using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractDropdownRepo 
    /// ================================================================= 
    public partial interface ISubcontractDropdownRepo
    {


        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractDropdown>> GetByDropDownName(string dropdownName);
     
    }
}
