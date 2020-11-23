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
    /// Description:	Class for the repo SubcontractProfileAddressRepo 
    /// =================================================================
    public partial class SubcontractProfileAddressRepo : ISubcontractProfileAddressRepo
    {

        private IDbContext _dbContext;

        public SubcontractProfileAddressRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>
            ("uspSubcontractProfileAddress_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress> GetByAddressId(string addressId)
        {
            var p = new DynamicParameters();
            p.Add("@address_id", addressId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>
            ("uspSubcontractProfileAddress_selectByAddressId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress subcontractProfileAddress)
        {
            var p = new DynamicParameters();

            p.Add("@address_id", subcontractProfileAddress.AddressId);
            p.Add("@house_no", subcontractProfileAddress.HouseNo);
            p.Add("@building", subcontractProfileAddress.Building);
            p.Add("@floor", subcontractProfileAddress.Floor);
            p.Add("@moo", subcontractProfileAddress.Moo);
            p.Add("@soi", subcontractProfileAddress.Soi);
            p.Add("@road", subcontractProfileAddress.Road);
            p.Add("@sub_district_id", subcontractProfileAddress.SubDistrictId);
            p.Add("@district_id", subcontractProfileAddress.DistrictId);
            p.Add("@province_id", subcontractProfileAddress.ProvinceId);
            p.Add("@region_id", subcontractProfileAddress.RegionId);
            p.Add("@address_type_id", subcontractProfileAddress.AddressTypeId);
            p.Add("@zip_code", subcontractProfileAddress.ZipCode);
            p.Add("@country", subcontractProfileAddress.Country);
            p.Add("@village_name", subcontractProfileAddress.VillageName);
            p.Add("@room_no", subcontractProfileAddress.RoomNo);
            p.Add("@company_id", subcontractProfileAddress. CompanyId);
            p.Add("@create_date", subcontractProfileAddress.CreateDate);
            p.Add("@create_by", subcontractProfileAddress.CreateBy);
            p.Add("@location_id", subcontractProfileAddress.LocationId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddress_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress subcontractProfileAddress)
        {
            var p = new DynamicParameters();
            p.Add("@address_id", subcontractProfileAddress.AddressId);
            p.Add("@house_no", subcontractProfileAddress.HouseNo);
            p.Add("@building", subcontractProfileAddress.Building);
            p.Add("@floor", subcontractProfileAddress.Floor);
            p.Add("@moo", subcontractProfileAddress.Moo);
            p.Add("@soi", subcontractProfileAddress.Soi);
            p.Add("@road", subcontractProfileAddress.Road);
            p.Add("@sub_district_id", subcontractProfileAddress.SubDistrictId);
            p.Add("@district_id", subcontractProfileAddress.DistrictId);
            p.Add("@province_id", subcontractProfileAddress.ProvinceId);
            p.Add("@region_id", subcontractProfileAddress.RegionId);
            p.Add("@address_type_id", subcontractProfileAddress.AddressTypeId);
            p.Add("@zip_code", subcontractProfileAddress.ZipCode);
            p.Add("@country", subcontractProfileAddress.Country);
            p.Add("@village_name", subcontractProfileAddress.VillageName);
            p.Add("@room_no", subcontractProfileAddress.RoomNo);
            p.Add("@company_id", subcontractProfileAddress.CompanyId);
            p.Add("@modified_date", subcontractProfileAddress.ModifiedDate);
            p.Add("@modified_by", subcontractProfileAddress.ModifiedBy);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddress_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string addressId)
        {
            var p = new DynamicParameters();
            p.Add("@address_id", addressId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddress_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress> subcontractProfileAddressList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileAddressDataTable(subcontractProfileAddressList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddress_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileAddressDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress> SubcontractProfileAddressList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("address_id", typeof(SqlGuid));
            dt.Columns.Add("house_no", typeof(SqlString));
            dt.Columns.Add("building", typeof(SqlString));
            dt.Columns.Add("floor", typeof(SqlString));
            dt.Columns.Add("moo", typeof(SqlInt32));
            dt.Columns.Add("soi", typeof(SqlString));
            dt.Columns.Add("road", typeof(SqlString));
            dt.Columns.Add("sub_district_id", typeof(SqlInt32));
            dt.Columns.Add("district_id", typeof(SqlInt32));
            dt.Columns.Add("province_id", typeof(SqlInt32));
            dt.Columns.Add("region_id", typeof(SqlInt32));
            dt.Columns.Add("address_type_id", typeof(SqlString));
            dt.Columns.Add("zip_code", typeof(SqlString));
            dt.Columns.Add("country", typeof(SqlString));
            dt.Columns.Add("village_name", typeof(SqlString));
            dt.Columns.Add("room_no", typeof(SqlString));
            dt.Columns.Add("company_id", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));

            if (SubcontractProfileAddressList != null)
                foreach (var curObj in SubcontractProfileAddressList)
                {
                    DataRow row = dt.NewRow();
                    row["address_id"] = new SqlGuid(curObj.AddressId);
                    row["house_no"] = new SqlString(curObj.HouseNo);
                    row["building"] = new SqlString(curObj.Building);
                    row["floor"] = new SqlString(curObj.Floor);
                    row["moo"] = curObj.Moo == null ? SqlInt32.Null : new SqlInt32((int)curObj.Moo.Value);
                    row["soi"] = new SqlString(curObj.Soi);
                    row["road"] = new SqlString(curObj.Road);
                    row["sub_district_id"] = curObj.SubDistrictId == null ? SqlInt32.Null : new SqlInt32((int)curObj.SubDistrictId.Value);
                    row["district_id"] = curObj.DistrictId == null ? SqlInt32.Null : new SqlInt32((int)curObj.DistrictId.Value);
                    row["province_id"] = curObj.ProvinceId == null ? SqlInt32.Null : new SqlInt32((int)curObj.ProvinceId.Value);
                    row["region_id"]= curObj.RegionId == null ? SqlInt32.Null : new SqlInt32((int)curObj.RegionId.Value);
                    row["address_type_id"] = new SqlString(curObj.AddressTypeId);
                    row["zip_code"]= new SqlString(curObj.ZipCode);
                    row["country"]= new SqlString(curObj.Country);
                    row["village_name"]= new SqlString(curObj.VillageName);
                    row["room_no"]= new SqlString(curObj.RoomNo);
                    row["company_id"]= new SqlString(curObj.CompanyId);
                    row["create_date"] = DateTime.Now;
                    row["create_by"]= new SqlString(curObj.CreateBy);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileAddressPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>
                ("uspSubcontractProfileAddress_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileAddressPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("address_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["address_id"] = new SqlGuid(curObj.AddressId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<bool> DeleteByCompanyId(string id)
        {
            var p = new DynamicParameters();
            p.Add("@companyId", id);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddress_deleteByCompanyId", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        public async Task<bool> DeleteByLocationId(string id)
        {
            var p = new DynamicParameters();
            p.Add("@locationid", id);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAddress_deleteByLocationId", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }
    }


}
