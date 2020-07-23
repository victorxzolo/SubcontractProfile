using Dapper;
using Repository;
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
    /// Description:	Class for the repo SubcontractProfileLocationRepo 
    /// =================================================================
    public partial class SubcontractProfileLocationRepo : ISubcontractProfileLocationRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileLocationRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>
            ("uspSubcontractProfileLocation_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> GetByLocationId(System.Guid locationId)
        {
            var p = new DynamicParameters();
            p.Add("@location_id", locationId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>
            ("uspSubcontractProfileLocation_selectByLocationId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation subcontractProfileLocation)
        {
            var p = new DynamicParameters();

            p.Add("@location_id", subcontractProfileLocation.LocationId);
            p.Add("@location_code", subcontractProfileLocation.LocationCode);
            p.Add("@location_name", subcontractProfileLocation.LocationName);
            p.Add("@location_name_th", subcontractProfileLocation.LocationNameTh);
            p.Add("@location_name_en", subcontractProfileLocation.LocationNameEn);
            p.Add("@location_name_alias", subcontractProfileLocation.LocationNameAlias);
            p.Add("@vendor_code", subcontractProfileLocation.VendorCode);
            p.Add("@storage_location", subcontractProfileLocation.StorageLocation);
            p.Add("@ship_to", subcontractProfileLocation.ShipTo);
            p.Add("@out_of_service_storage_location", subcontractProfileLocation.OutOfServiceStorageLocation);
            p.Add("@sub_phase", subcontractProfileLocation.SubPhase);
            p.Add("@effective_date", subcontractProfileLocation.EffectiveDate);
            p.Add("@shop_type", subcontractProfileLocation.ShopType);
            p.Add("@vat_branch_number", subcontractProfileLocation.VatBranchNumber);
            p.Add("@phone", subcontractProfileLocation.Phone);
            p.Add("@company_main_contract_phone", subcontractProfileLocation.CompanyMainContractPhone);
            p.Add("@installations_contract_phone", subcontractProfileLocation.InstallationsContractPhone);
            p.Add("@maintenance_contract_phone", subcontractProfileLocation.MaintenanceContractPhone);
            p.Add("@inventory_contract_phone", subcontractProfileLocation.InventoryContractPhone);
            p.Add("@payment_contract_phone", subcontractProfileLocation.PaymentContractPhone);
            p.Add("@etc_contract_phone", subcontractProfileLocation.EtcContractPhone);
            p.Add("@company_group_mail", subcontractProfileLocation.CompanyGroupMail);
            p.Add("@installations_contract_mail", subcontractProfileLocation.InstallationsContractMail);
            p.Add("@maintenance_contract_mail", subcontractProfileLocation.MaintenanceContractMail);
            p.Add("@inventory_contract_mail", subcontractProfileLocation.InventoryContractMail);
            p.Add("@payment_contract_mail", subcontractProfileLocation.PaymentContractMail);
            p.Add("@etc_contract_mail", subcontractProfileLocation.EtcContractMail);
            p.Add("@location_address", subcontractProfileLocation.LocationAddress);
            p.Add("@post_address", subcontractProfileLocation.PostAddress);
            p.Add("@tax_address", subcontractProfileLocation.TaxAddress);
            p.Add("@wt_address", subcontractProfileLocation.WtAddress);
            p.Add("@house_no", subcontractProfileLocation.HouseNo);
            p.Add("@area_code", subcontractProfileLocation.AreaCode);
            p.Add("@bank_code", subcontractProfileLocation.BankCode);
            p.Add("@bank_name", subcontractProfileLocation.BankName);
            p.Add("@bank_account_no", subcontractProfileLocation.BankAccountNo);
            p.Add("@bank_account_name", subcontractProfileLocation.BankAccountName);
            p.Add("@bank_attach_file", subcontractProfileLocation.BankAttachFile);
            p.Add("@status", subcontractProfileLocation.Status);
            p.Add("@create_date", subcontractProfileLocation.CreateDate);
            p.Add("@create_by", subcontractProfileLocation.CreateBy);
            p.Add("@update_by", subcontractProfileLocation.UpdateBy);
            p.Add("@update_date", subcontractProfileLocation.UpdateDate);
            p.Add("@bank_branch_code", subcontractProfileLocation.BankBranchCode);
            p.Add("@bank_branch_name", subcontractProfileLocation.BankBranchName);
            p.Add("@penalty_contract_phone", subcontractProfileLocation.PenaltyContractPhone);
            p.Add("@penalty_contract_mail", subcontractProfileLocation.PenaltyContractMail);
            p.Add("@contract_phone", subcontractProfileLocation.ContractPhone);
            p.Add("@contract_mail", subcontractProfileLocation.ContractMail);
            p.Add("@company_id", subcontractProfileLocation.CompanyId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileLocation_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation subcontractProfileLocation)
        {
            var p = new DynamicParameters();
            p.Add("@location_id", subcontractProfileLocation.LocationId);
            p.Add("@location_code", subcontractProfileLocation.LocationCode);
            p.Add("@location_name", subcontractProfileLocation.LocationName);
            p.Add("@location_name_th", subcontractProfileLocation.LocationNameTh);
            p.Add("@location_name_en", subcontractProfileLocation.LocationNameEn);
            p.Add("@location_name_alias", subcontractProfileLocation.LocationNameAlias);
            p.Add("@vendor_code", subcontractProfileLocation.VendorCode);
            p.Add("@storage_location", subcontractProfileLocation.StorageLocation);
            p.Add("@ship_to", subcontractProfileLocation.ShipTo);
            p.Add("@out_of_service_storage_location", subcontractProfileLocation.OutOfServiceStorageLocation);
            p.Add("@sub_phase", subcontractProfileLocation.SubPhase);
            p.Add("@effective_date", subcontractProfileLocation.EffectiveDate);
            p.Add("@shop_type", subcontractProfileLocation.ShopType);
            p.Add("@vat_branch_number", subcontractProfileLocation.VatBranchNumber);
            p.Add("@phone", subcontractProfileLocation.Phone);
            p.Add("@company_main_contract_phone", subcontractProfileLocation.CompanyMainContractPhone);
            p.Add("@installations_contract_phone", subcontractProfileLocation.InstallationsContractPhone);
            p.Add("@maintenance_contract_phone", subcontractProfileLocation.MaintenanceContractPhone);
            p.Add("@inventory_contract_phone", subcontractProfileLocation.InventoryContractPhone);
            p.Add("@payment_contract_phone", subcontractProfileLocation.PaymentContractPhone);
            p.Add("@etc_contract_phone", subcontractProfileLocation.EtcContractPhone);
            p.Add("@company_group_mail", subcontractProfileLocation.CompanyGroupMail);
            p.Add("@installations_contract_mail", subcontractProfileLocation.InstallationsContractMail);
            p.Add("@maintenance_contract_mail", subcontractProfileLocation.MaintenanceContractMail);
            p.Add("@inventory_contract_mail", subcontractProfileLocation.InventoryContractMail);
            p.Add("@payment_contract_mail", subcontractProfileLocation.PaymentContractMail);
            p.Add("@etc_contract_mail", subcontractProfileLocation.EtcContractMail);
            p.Add("@location_address", subcontractProfileLocation.LocationAddress);
            p.Add("@post_address", subcontractProfileLocation.PostAddress);
            p.Add("@tax_address", subcontractProfileLocation.TaxAddress);
            p.Add("@wt_address", subcontractProfileLocation.WtAddress);
            p.Add("@house_no", subcontractProfileLocation.HouseNo);
            p.Add("@area_code", subcontractProfileLocation.AreaCode);
            p.Add("@bank_code", subcontractProfileLocation.BankCode);
            p.Add("@bank_name", subcontractProfileLocation.BankName);
            p.Add("@bank_account_no", subcontractProfileLocation.BankAccountNo);
            p.Add("@bank_account_name", subcontractProfileLocation.BankAccountName);
            p.Add("@bank_attach_file", subcontractProfileLocation.BankAttachFile);
            p.Add("@status", subcontractProfileLocation.Status);
            p.Add("@create_date", subcontractProfileLocation.CreateDate);
            p.Add("@create_by", subcontractProfileLocation.CreateBy);
            p.Add("@update_by", subcontractProfileLocation.UpdateBy);
            p.Add("@update_date", subcontractProfileLocation.UpdateDate);
            p.Add("@bank_branch_code", subcontractProfileLocation.BankBranchCode);
            p.Add("@bank_branch_name", subcontractProfileLocation.BankBranchName);
            p.Add("@penalty_contract_phone", subcontractProfileLocation.PenaltyContractPhone);
            p.Add("@penalty_contract_mail", subcontractProfileLocation.PenaltyContractMail);
            p.Add("@contract_phone", subcontractProfileLocation.ContractPhone);
            p.Add("@contract_mail", subcontractProfileLocation.ContractMail);
            p.Add("@company_id", subcontractProfileLocation.CompanyId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileLocation_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string locationId)
        {
            var p = new DynamicParameters();
            p.Add("@location_id", locationId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileLocation_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> subcontractProfileLocationList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileLocationDataTable(subcontractProfileLocationList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileLocation_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileLocationDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation> SubcontractProfileLocationList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("location_id", typeof(SqlGuid));
            dt.Columns.Add("location_code", typeof(SqlString));
            dt.Columns.Add("location_name", typeof(SqlString));
            dt.Columns.Add("location_name_th", typeof(SqlString));
            dt.Columns.Add("location_name_en", typeof(SqlString));
            dt.Columns.Add("location_name_alias", typeof(SqlString));
            dt.Columns.Add("vendor_code", typeof(SqlString));
            dt.Columns.Add("storage_location", typeof(SqlString));
            dt.Columns.Add("ship_to", typeof(SqlString));
            dt.Columns.Add("out_of_service_storage_location", typeof(SqlString));
            dt.Columns.Add("sub_phase", typeof(SqlString));
            dt.Columns.Add("effective_date", typeof(SqlDateTime));
            dt.Columns.Add("shop_type", typeof(SqlString));
            dt.Columns.Add("vat_branch_number", typeof(SqlString));
            dt.Columns.Add("phone", typeof(SqlString));
            dt.Columns.Add("company_main_contract_phone", typeof(SqlString));
            dt.Columns.Add("installations_contract_phone", typeof(SqlString));
            dt.Columns.Add("maintenance_contract_phone", typeof(SqlString));
            dt.Columns.Add("inventory_contract_phone", typeof(SqlString));
            dt.Columns.Add("payment_contract_phone", typeof(SqlString));
            dt.Columns.Add("etc_contract_phone", typeof(SqlString));
            dt.Columns.Add("company_group_mail", typeof(SqlString));
            dt.Columns.Add("installations_contract_mail", typeof(SqlString));
            dt.Columns.Add("maintenance_contract_mail", typeof(SqlString));
            dt.Columns.Add("inventory_contract_mail", typeof(SqlString));
            dt.Columns.Add("payment_contract_mail", typeof(SqlString));
            dt.Columns.Add("etc_contract_mail", typeof(SqlString));
            dt.Columns.Add("location_address", typeof(SqlString));
            dt.Columns.Add("post_address", typeof(SqlString));
            dt.Columns.Add("tax_address", typeof(SqlString));
            dt.Columns.Add("wt_address", typeof(SqlString));
            dt.Columns.Add("house_no", typeof(SqlString));
            dt.Columns.Add("area_code", typeof(SqlString));
            dt.Columns.Add("bank_code", typeof(SqlString));
            dt.Columns.Add("bank_name", typeof(SqlString));
            dt.Columns.Add("bank_account_no", typeof(SqlString));
            dt.Columns.Add("bank_account_name", typeof(SqlString));
            dt.Columns.Add("bank_attach_file", typeof(SqlString));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("update_by", typeof(SqlString));
            dt.Columns.Add("update_date", typeof(SqlDateTime));
            dt.Columns.Add("bank_branch_code", typeof(SqlString));
            dt.Columns.Add("bank_branch_name", typeof(SqlString));
            dt.Columns.Add("penalty_contract_phone", typeof(SqlString));
            dt.Columns.Add("penalty_contract_mail", typeof(SqlString));
            dt.Columns.Add("contract_phone", typeof(SqlString));
            dt.Columns.Add("contract_mail", typeof(SqlString));
            dt.Columns.Add("company_id", typeof(SqlString));

            if (SubcontractProfileLocationList != null)
                foreach (var curObj in SubcontractProfileLocationList)
                {
                    DataRow row = dt.NewRow();
                    row["location_id"] = new SqlGuid(curObj.LocationId);
                    row["location_code"] = new SqlString(curObj.LocationCode);
                    row["location_name"] = new SqlString(curObj.LocationName);
                    row["location_name_th"] = new SqlString(curObj.LocationNameTh);
                    row["location_name_en"] = new SqlString(curObj.LocationNameEn);
                    row["location_name_alias"] = new SqlString(curObj.LocationNameAlias);
                    row["vendor_code"] = new SqlString(curObj.VendorCode);
                    row["storage_location"] = new SqlString(curObj.StorageLocation);
                    row["ship_to"] = new SqlString(curObj.ShipTo);
                    row["out_of_service_storage_location"] = new SqlString(curObj.OutOfServiceStorageLocation);
                    row["sub_phase"] = new SqlString(curObj.SubPhase);
                    row["effective_date"] = curObj.EffectiveDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.EffectiveDate.Value);
                    row["shop_type"] = new SqlString(curObj.ShopType);
                    row["vat_branch_number"] = new SqlString(curObj.VatBranchNumber);
                    row["phone"] = new SqlString(curObj.Phone);
                    row["company_main_contract_phone"] = new SqlString(curObj.CompanyMainContractPhone);
                    row["installations_contract_phone"] = new SqlString(curObj.InstallationsContractPhone);
                    row["maintenance_contract_phone"] = new SqlString(curObj.MaintenanceContractPhone);
                    row["inventory_contract_phone"] = new SqlString(curObj.InventoryContractPhone);
                    row["payment_contract_phone"] = new SqlString(curObj.PaymentContractPhone);
                    row["etc_contract_phone"] = new SqlString(curObj.EtcContractPhone);
                    row["company_group_mail"] = new SqlString(curObj.CompanyGroupMail);
                    row["installations_contract_mail"] = new SqlString(curObj.InstallationsContractMail);
                    row["maintenance_contract_mail"] = new SqlString(curObj.MaintenanceContractMail);
                    row["inventory_contract_mail"] = new SqlString(curObj.InventoryContractMail);
                    row["payment_contract_mail"] = new SqlString(curObj.PaymentContractMail);
                    row["etc_contract_mail"] = new SqlString(curObj.EtcContractMail);
                    row["location_address"] = new SqlString(curObj.LocationAddress);
                    row["post_address"] = new SqlString(curObj.PostAddress);
                    row["tax_address"] = new SqlString(curObj.TaxAddress);
                    row["wt_address"] = new SqlString(curObj.WtAddress);
                    row["house_no"] = new SqlString(curObj.HouseNo);
                    row["area_code"] = new SqlString(curObj.AreaCode);
                    row["bank_code"] = new SqlString(curObj.BankCode);
                    row["bank_name"] = new SqlString(curObj.BankName);
                    row["bank_account_no"] = new SqlString(curObj.BankAccountNo);
                    row["bank_account_name"] = new SqlString(curObj.BankAccountName);
                    row["bank_attach_file"] = new SqlString(curObj.BankAttachFile);
                    row["status"] = new SqlString(curObj.Status);
                    row["create_date"] = new SqlDateTime(curObj.CreateDate);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["update_by"] = new SqlString(curObj.UpdateBy);
                    row["update_date"] = curObj.UpdateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.UpdateDate.Value);
                    row["bank_branch_code"] = new SqlString(curObj.BankBranchCode);
                    row["bank_branch_name"] = new SqlString(curObj.BankBranchName);
                    row["penalty_contract_phone"] = new SqlString(curObj.PenaltyContractPhone);
                    row["penalty_contract_mail"] = new SqlString(curObj.PenaltyContractMail);
                    row["contract_phone"] = new SqlString(curObj.ContractPhone);
                    row["contract_mail"] = new SqlString(curObj.ContractMail);
                    row["company_id"] = new SqlString(curObj.CompanyId);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileLocationPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation>
                ("uspSubcontractProfileLocation_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileLocationPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileLocation_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("location_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["location_id"] = new SqlGuid(curObj.LocationId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
