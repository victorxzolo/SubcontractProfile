using SubcontractProfile.WebApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlTypes;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.Services.Services
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Class for the repo SubcontractDropdownRepo 
    /// =================================================================
    public partial class SubcontractDropdownRepo : ISubcontractDropdownRepo
    {

        private IDbContext _dbContext;

        public SubcontractDropdownRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get by name
        /// </summary>
        public async Task<IEnumerable<SubcontractDropdown>> GetByDropDownName(string dropdownName) { 
            var p = new DynamicParameters();
            p.Add("@dropdown_name", dropdownName);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractDropdown>
            ("uspSubcontractDropdown_selectByDropDownName", p, commandType: CommandType.StoredProcedure);

            return entity;
        }
    }
}
