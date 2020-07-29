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
    /// Author: AIS Fibre -X10
    /// Description:	Class for the repo SubcontractProfileAddressTypeRepo 
    /// =================================================================
    public partial class SubcontractProfileAddressTypeRepo : ISubcontractProfileAddressTypeRepo
    {

        protected IDbContext _dbContext = null;

        public SubcontractProfileAddressTypeRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType>
            ("uspSubcontractProfileAddressType_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType> GetByAddressTypeId(string addressTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@address_type_id", addressTypeId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType>
            ("uspSubcontractProfileAddressType_selectByAddressTypeId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType subcontractProfileAddressType)
        {
            var p = new DynamicParameters();

            p.Add("@address_type_id", subcontractProfileAddressType.AddressTypeId);
            p.Add("@address_type_name_th", subcontractProfileAddressType.AddressTypeNameTh);
            p.Add("@address_type_name_en", subcontractProfileAddressType.AddressTypeNameEn);
            p.Add("@create_by", subcontractProfileAddressType.CreateBy);
            p.Add("@create_date", subcontractProfileAddressType.CreateDate);
            p.Add("@modified_by", subcontractProfileAddressType.ModifiedBy);
            p.Add("@modified_date", subcontractProfileAddressType.ModifiedDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddressType_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType subcontractProfileAddressType)
        {
            var p = new DynamicParameters();
            p.Add("@address_type_id", subcontractProfileAddressType.AddressTypeId);
            p.Add("@address_type_name_th", subcontractProfileAddressType.AddressTypeNameTh);
            p.Add("@address_type_name_en", subcontractProfileAddressType.AddressTypeNameEn);
            p.Add("@create_by", subcontractProfileAddressType.CreateBy);
            p.Add("@create_date", subcontractProfileAddressType.CreateDate);
            p.Add("@modified_by", subcontractProfileAddressType.ModifiedBy);
            p.Add("@modified_date", subcontractProfileAddressType.ModifiedDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddressType_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string addressTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@address_type_id", addressTypeId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddressType_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType> subcontractProfileAddressTypeList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileAddressTypeDataTable(subcontractProfileAddressTypeList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddressType_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileAddressTypeDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType> SubcontractProfileAddressTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("address_type_id", typeof(SqlString));
            dt.Columns.Add("address_type_name_th", typeof(SqlString));
            dt.Columns.Add("address_type_name_en", typeof(SqlString));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("modified_by", typeof(SqlString));
            dt.Columns.Add("modified_date", typeof(SqlDateTime));

            if (SubcontractProfileAddressTypeList != null)
                foreach (var curObj in SubcontractProfileAddressTypeList)
                {
                    DataRow row = dt.NewRow();
                    row["address_type_id"] = new SqlString(curObj.AddressTypeId);
                    row["address_type_name_th"] = new SqlString(curObj.AddressTypeNameTh);
                    row["address_type_name_en"] = new SqlString(curObj.AddressTypeNameEn);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["modified_by"] = new SqlString(curObj.ModifiedBy);
                    row["modified_date"] = curObj.ModifiedDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ModifiedDate.Value);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
