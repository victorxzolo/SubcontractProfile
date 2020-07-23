using Dapper;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Services
{

    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Class for the repo SubcontractProfileTeamRepo 
    /// =================================================================
    public partial class SubcontractProfileTeamRepo : ISubcontractProfileTeamRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileTeamRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>
            ("uspSubcontractProfileTeam_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> GetByTeamId(System.Guid teamId)
        {
            var p = new DynamicParameters();
            p.Add("@team_id", teamId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>
            ("uspSubcontractProfileTeam_selectByTeamId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam)
        {
            var p = new DynamicParameters();

            p.Add("@team_id", subcontractProfileTeam.TeamId);
            p.Add("@team_code", subcontractProfileTeam.TeamCode);
            p.Add("@team_name", subcontractProfileTeam.TeamName);
            p.Add("@team_name_th", subcontractProfileTeam.TeamNameTh);
            p.Add("@team_name_en", subcontractProfileTeam.TeamNameEn);
            p.Add("@ship_to", subcontractProfileTeam.ShipTo);
            p.Add("@stage_local", subcontractProfileTeam.StageLocal);
            p.Add("@oos_storage_location", subcontractProfileTeam.OosStorageLocation);
            p.Add("@location_code", subcontractProfileTeam.LocationCode);
            p.Add("@vendor_code", subcontractProfileTeam.VendorCode);
            p.Add("@job_type", subcontractProfileTeam.JobType);
            p.Add("@subcontract_type", subcontractProfileTeam.SubcontractType);
            p.Add("@subcontract_sub_type", subcontractProfileTeam.SubcontractSubType);
            p.Add("@warranty_ma", subcontractProfileTeam.WarrantyMa);
            p.Add("@warranty_install", subcontractProfileTeam.WarrantyInstall);
            p.Add("@service_skill", subcontractProfileTeam.ServiceSkill);
            p.Add("@installations_contract_phone", subcontractProfileTeam.InstallationsContractPhone);
            p.Add("@maintenance_contract_phone", subcontractProfileTeam.MaintenanceContractPhone);
            p.Add("@etc_contract_phone", subcontractProfileTeam.EtcContractPhone);
            p.Add("@installations_contract_email", subcontractProfileTeam.InstallationsContractEmail);
            p.Add("@maintenance_contract_email", subcontractProfileTeam.MaintenanceContractEmail);
            p.Add("@etc_contract_email", subcontractProfileTeam.EtcContractEmail);
            p.Add("@status", subcontractProfileTeam.Status);
            p.Add("@create_date", subcontractProfileTeam.CreateDate);
            p.Add("@create_by", subcontractProfileTeam.CreateBy);
            p.Add("@update_by", subcontractProfileTeam.UpdateBy);
            p.Add("@update_date", subcontractProfileTeam.UpdateDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTeam_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam)
        {
            var p = new DynamicParameters();
            p.Add("@team_id", subcontractProfileTeam.TeamId);
            p.Add("@team_code", subcontractProfileTeam.TeamCode);
            p.Add("@team_name", subcontractProfileTeam.TeamName);
            p.Add("@team_name_th", subcontractProfileTeam.TeamNameTh);
            p.Add("@team_name_en", subcontractProfileTeam.TeamNameEn);
            p.Add("@ship_to", subcontractProfileTeam.ShipTo);
            p.Add("@stage_local", subcontractProfileTeam.StageLocal);
            p.Add("@oos_storage_location", subcontractProfileTeam.OosStorageLocation);
            p.Add("@location_code", subcontractProfileTeam.LocationCode);
            p.Add("@vendor_code", subcontractProfileTeam.VendorCode);
            p.Add("@job_type", subcontractProfileTeam.JobType);
            p.Add("@subcontract_type", subcontractProfileTeam.SubcontractType);
            p.Add("@subcontract_sub_type", subcontractProfileTeam.SubcontractSubType);
            p.Add("@warranty_ma", subcontractProfileTeam.WarrantyMa);
            p.Add("@warranty_install", subcontractProfileTeam.WarrantyInstall);
            p.Add("@service_skill", subcontractProfileTeam.ServiceSkill);
            p.Add("@installations_contract_phone", subcontractProfileTeam.InstallationsContractPhone);
            p.Add("@maintenance_contract_phone", subcontractProfileTeam.MaintenanceContractPhone);
            p.Add("@etc_contract_phone", subcontractProfileTeam.EtcContractPhone);
            p.Add("@installations_contract_email", subcontractProfileTeam.InstallationsContractEmail);
            p.Add("@maintenance_contract_email", subcontractProfileTeam.MaintenanceContractEmail);
            p.Add("@etc_contract_email", subcontractProfileTeam.EtcContractEmail);
            p.Add("@status", subcontractProfileTeam.Status);
            p.Add("@create_date", subcontractProfileTeam.CreateDate);
            p.Add("@create_by", subcontractProfileTeam.CreateBy);
            p.Add("@update_by", subcontractProfileTeam.UpdateBy);
            p.Add("@update_date", subcontractProfileTeam.UpdateDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTeam_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string teamId)
        {
            var p = new DynamicParameters();
            p.Add("@team_id", teamId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTeam_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> subcontractProfileTeamList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileTeamDataTable(subcontractProfileTeamList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTeam_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileTeamDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> SubcontractProfileTeamList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("team_id", typeof(SqlGuid));
            dt.Columns.Add("team_code", typeof(SqlString));
            dt.Columns.Add("team_name", typeof(SqlString));
            dt.Columns.Add("team_name_th", typeof(SqlString));
            dt.Columns.Add("team_name_en", typeof(SqlString));
            dt.Columns.Add("ship_to", typeof(SqlString));
            dt.Columns.Add("stage_local", typeof(SqlString));
            dt.Columns.Add("oos_storage_location", typeof(SqlString));
            dt.Columns.Add("location_code", typeof(SqlString));
            dt.Columns.Add("vendor_code", typeof(SqlString));
            dt.Columns.Add("job_type", typeof(SqlString));
            dt.Columns.Add("subcontract_type", typeof(SqlString));
            dt.Columns.Add("subcontract_sub_type", typeof(SqlString));
            dt.Columns.Add("warranty_ma", typeof(SqlString));
            dt.Columns.Add("warranty_install", typeof(SqlString));
            dt.Columns.Add("service_skill", typeof(SqlString));
            dt.Columns.Add("installations_contract_phone", typeof(SqlString));
            dt.Columns.Add("maintenance_contract_phone", typeof(SqlString));
            dt.Columns.Add("etc_contract_phone", typeof(SqlString));
            dt.Columns.Add("installations_contract_email", typeof(SqlString));
            dt.Columns.Add("maintenance_contract_email", typeof(SqlString));
            dt.Columns.Add("etc_contract_email", typeof(SqlString));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("update_by", typeof(SqlString));
            dt.Columns.Add("update_date", typeof(SqlDateTime));

            if (SubcontractProfileTeamList != null)
                foreach (var curObj in SubcontractProfileTeamList)
                {
                    DataRow row = dt.NewRow();
                    row["team_id"] = new SqlGuid(curObj.TeamId);
                    row["team_code"] = new SqlString(curObj.TeamCode);
                    row["team_name"] = new SqlString(curObj.TeamName);
                    row["team_name_th"] = new SqlString(curObj.TeamNameTh);
                    row["team_name_en"] = new SqlString(curObj.TeamNameEn);
                    row["ship_to"] = new SqlString(curObj.ShipTo);
                    row["stage_local"] = new SqlString(curObj.StageLocal);
                    row["oos_storage_location"] = new SqlString(curObj.OosStorageLocation);
                    row["location_code"] = new SqlString(curObj.LocationCode);
                    row["vendor_code"] = new SqlString(curObj.VendorCode);
                    row["job_type"] = new SqlString(curObj.JobType);
                    row["subcontract_type"] = new SqlString(curObj.SubcontractType);
                    row["subcontract_sub_type"] = new SqlString(curObj.SubcontractSubType);
                    row["warranty_ma"] = new SqlString(curObj.WarrantyMa);
                    row["warranty_install"] = new SqlString(curObj.WarrantyInstall);
                    row["service_skill"] = new SqlString(curObj.ServiceSkill);
                    row["installations_contract_phone"] = new SqlString(curObj.InstallationsContractPhone);
                    row["maintenance_contract_phone"] = new SqlString(curObj.MaintenanceContractPhone);
                    row["etc_contract_phone"] = new SqlString(curObj.EtcContractPhone);
                    row["installations_contract_email"] = new SqlString(curObj.InstallationsContractEmail);
                    row["maintenance_contract_email"] = new SqlString(curObj.MaintenanceContractEmail);
                    row["etc_contract_email"] = new SqlString(curObj.EtcContractEmail);
                    row["status"] = new SqlString(curObj.Status);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["update_by"] = new SqlString(curObj.UpdateBy);
                    row["update_date"] = curObj.UpdateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.UpdateDate.Value);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileTeamPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>
                ("uspSubcontractProfileTeam_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileTeamPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("team_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["team_id"] = new SqlGuid(curObj.TeamId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
