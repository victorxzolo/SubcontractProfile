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

namespace SubcontractProfile.WebApi.Services
{

    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Class for the repo SubcontractProfileCompanyRepo 
    /// =================================================================
    public partial class SubcontractProfileCompanyRepo : ISubcontractProfileCompanyRepo
    {

        // protected Repository.DbContext _dbContext = null;
        private IDbContext _dbContext;

        public SubcontractProfileCompanyRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>
            ("uspSubcontractProfileCompany_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> GetByCompanyId(System.Guid companyId)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", companyId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>
            ("uspSubcontractProfileCompany_selectByCompanyId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany)
        {
            var p = new DynamicParameters();

            p.Add("@company_id", subcontractProfileCompany.CompanyId);
            p.Add("@company_code", subcontractProfileCompany.CompanyCode);
            p.Add("@company_name", subcontractProfileCompany.CompanyName);
            p.Add("@company_name_th", subcontractProfileCompany.CompanyNameTh);
            p.Add("@company_name_en", subcontractProfileCompany.CompanyNameEn);
            p.Add("@company_alias", subcontractProfileCompany.CompanyAlias);
            p.Add("@distribution_channel", subcontractProfileCompany.DistributionChannel);
            p.Add("@channel_sale_group", subcontractProfileCompany.ChannelSaleGroup);
            p.Add("@vendor_code", subcontractProfileCompany.VendorCode);
            p.Add("@customer_code", subcontractProfileCompany.CustomerCode);
            p.Add("@area_id", subcontractProfileCompany.AreaId);
            p.Add("@tax_id", subcontractProfileCompany.TaxId);
            p.Add("@wt_name", subcontractProfileCompany.WtName);
            p.Add("@vat_type", subcontractProfileCompany.VatType);
            p.Add("@company_certified_file", subcontractProfileCompany.CompanyCertifiedFile);
            p.Add("@commercial_registration_file", subcontractProfileCompany.CommercialRegistrationFile);
            p.Add("@vat_registration_certificate_file", subcontractProfileCompany.VatRegistrationCertificateFile);
            p.Add("@contract_agreement_file", subcontractProfileCompany.ContractAgreementFile);
            p.Add("@deposit_authorization_level", subcontractProfileCompany.DepositAuthorizationLevel);
            p.Add("@deposit_payment_type", subcontractProfileCompany.DepositPaymentType);
            p.Add("@contract_start_date", subcontractProfileCompany.ContractStartDate);
            p.Add("@contract_end_date", subcontractProfileCompany.ContractEndDate);
            p.Add("@over_draft_deposit", subcontractProfileCompany.OverDraftDeposit);
            p.Add("@balance_deposit", subcontractProfileCompany.BalanceDeposit);
            p.Add("@company_status", subcontractProfileCompany.CompanyStatus);
            p.Add("@company_address", subcontractProfileCompany.CompanyAddress);
            p.Add("@vat_address", subcontractProfileCompany.VatAddress);
            p.Add("@create_by", subcontractProfileCompany.CreateBy);
            p.Add("@create_date", subcontractProfileCompany.CreateDate);
            p.Add("@update_by", subcontractProfileCompany.UpdateBy);
            p.Add("@update_date", subcontractProfileCompany.UpdateDate);
            p.Add("@company_email", subcontractProfileCompany.CompanyEmail);
            p.Add("@contract_name", subcontractProfileCompany.ContractName);
            p.Add("@contract_phone", subcontractProfileCompany.ContractPhone);
            p.Add("@contract_email", subcontractProfileCompany.ContractEmail);
            p.Add("@bank_code", subcontractProfileCompany.BankCode);
            p.Add("@bank_name", subcontractProfileCompany.BankName);
            p.Add("@account_number", subcontractProfileCompany.AccountNumber);
            p.Add("@account_name", subcontractProfileCompany.AccountName);
            p.Add("@attach_file", subcontractProfileCompany.AttachFile);
            p.Add("@branch_code", subcontractProfileCompany.BranchCode);
            p.Add("@branch_name", subcontractProfileCompany.BranchName);
            p.Add("@dept_of_install_name", subcontractProfileCompany.DeptOfInstallName);
            p.Add("@dept_of_mainten_name", subcontractProfileCompany.DeptOfMaintenName);
            p.Add("@dept_of_account_name", subcontractProfileCompany.DeptOfAccountName);
            p.Add("@dept_of_install_phone", subcontractProfileCompany.DeptOfInstallPhone);
            p.Add("@dept_of_mainten_phone", subcontractProfileCompany.DeptOfMaintenPhone);
            p.Add("@dept_of_account_phone", subcontractProfileCompany.DeptOfAccountPhone);
            p.Add("@dept_of_install_email", subcontractProfileCompany.DeptOfInstallEmail);
            p.Add("@dept_of_mainten_email", subcontractProfileCompany.DeptOfMaintenEmail);
            p.Add("@dept_of_account_email", subcontractProfileCompany.DeptOfAccountEmail);
            p.Add("@location_code", subcontractProfileCompany.LocationCode);
            p.Add("@location_name_th", subcontractProfileCompany.LocationNameTh);
            p.Add("@location_name_en", subcontractProfileCompany.LocationNameEn);
            p.Add("@bank_account_type_id", subcontractProfileCompany.BankAccountTypeId);
            p.Add("@subcontract_profile_type", subcontractProfileCompany.SubcontractProfileType);
            p.Add("@company_title_th_id", subcontractProfileCompany.CompanyTitleThId);
            p.Add("@company_title_en_id", subcontractProfileCompany.CompanyTitleEnId);
            p.Add("@status", subcontractProfileCompany.Status);
            p.Add("@activate_date", subcontractProfileCompany.ActivateDate);
            p.Add("@user_name", subcontractProfileCompany.User_name);
            p.Add("@password", subcontractProfileCompany.Password);
           

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompany_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", subcontractProfileCompany.CompanyId);
            p.Add("@company_code", subcontractProfileCompany.CompanyCode);
            p.Add("@company_name", subcontractProfileCompany.CompanyName);
            p.Add("@company_name_th", subcontractProfileCompany.CompanyNameTh);
            p.Add("@company_name_en", subcontractProfileCompany.CompanyNameEn);
            p.Add("@company_alias", subcontractProfileCompany.CompanyAlias);
            p.Add("@distribution_channel", subcontractProfileCompany.DistributionChannel);
            p.Add("@channel_sale_group", subcontractProfileCompany.ChannelSaleGroup);
            p.Add("@vendor_code", subcontractProfileCompany.VendorCode);
            p.Add("@customer_code", subcontractProfileCompany.CustomerCode);
            p.Add("@area_id", subcontractProfileCompany.AreaId);
            p.Add("@tax_id", subcontractProfileCompany.TaxId);
            p.Add("@wt_name", subcontractProfileCompany.WtName);
            p.Add("@vat_type", subcontractProfileCompany.VatType);
            p.Add("@company_certified_file", subcontractProfileCompany.CompanyCertifiedFile);
            p.Add("@commercial_registration_file", subcontractProfileCompany.CommercialRegistrationFile);
            p.Add("@vat_registration_certificate_file", subcontractProfileCompany.VatRegistrationCertificateFile);
            p.Add("@contract_agreement_file", subcontractProfileCompany.ContractAgreementFile);
            p.Add("@deposit_authorization_level", subcontractProfileCompany.DepositAuthorizationLevel);
            p.Add("@deposit_payment_type", subcontractProfileCompany.DepositPaymentType);
            p.Add("@contract_start_date", subcontractProfileCompany.ContractStartDate);
            p.Add("@contract_end_date", subcontractProfileCompany.ContractEndDate);
            p.Add("@over_draft_deposit", subcontractProfileCompany.OverDraftDeposit);
            p.Add("@balance_deposit", subcontractProfileCompany.BalanceDeposit);
            p.Add("@company_status", subcontractProfileCompany.CompanyStatus);
            p.Add("@company_address", subcontractProfileCompany.CompanyAddress);
            p.Add("@vat_address", subcontractProfileCompany.VatAddress);
            p.Add("@create_by", subcontractProfileCompany.CreateBy);
            p.Add("@create_date", subcontractProfileCompany.CreateDate);
            p.Add("@update_by", subcontractProfileCompany.UpdateBy);
            p.Add("@update_date", subcontractProfileCompany.UpdateDate);
            p.Add("@company_email", subcontractProfileCompany.CompanyEmail);
            p.Add("@contract_name", subcontractProfileCompany.ContractName);
            p.Add("@contract_phone", subcontractProfileCompany.ContractPhone);
            p.Add("@contract_email", subcontractProfileCompany.ContractEmail);
            p.Add("@bank_code", subcontractProfileCompany.BankCode);
            p.Add("@bank_name", subcontractProfileCompany.BankName);
            p.Add("@account_number", subcontractProfileCompany.AccountNumber);
            p.Add("@account_name", subcontractProfileCompany.AccountName);
            p.Add("@attach_file", subcontractProfileCompany.AttachFile);
            p.Add("@branch_code", subcontractProfileCompany.BranchCode);
            p.Add("@branch_name", subcontractProfileCompany.BranchName);
            p.Add("@dept_of_install_name", subcontractProfileCompany.DeptOfInstallName);
            p.Add("@dept_of_mainten_name", subcontractProfileCompany.DeptOfMaintenName);
            p.Add("@dept_of_account_name", subcontractProfileCompany.DeptOfAccountName);
            p.Add("@dept_of_install_phone", subcontractProfileCompany.DeptOfInstallPhone);
            p.Add("@dept_of_mainten_phone", subcontractProfileCompany.DeptOfMaintenPhone);
            p.Add("@dept_of_account_phone", subcontractProfileCompany.DeptOfAccountPhone);
            p.Add("@dept_of_install_email", subcontractProfileCompany.DeptOfInstallEmail);
            p.Add("@dept_of_mainten_email", subcontractProfileCompany.DeptOfMaintenEmail);
            p.Add("@dept_of_account_email", subcontractProfileCompany.DeptOfAccountEmail);
            p.Add("@location_code", subcontractProfileCompany.LocationCode);
            p.Add("@location_name_th", subcontractProfileCompany.LocationNameTh);
            p.Add("@location_name_en", subcontractProfileCompany.LocationNameEn);
            p.Add("@bank_account_type_id", subcontractProfileCompany.BankAccountTypeId);
            p.Add("@subcontract_profile_type", subcontractProfileCompany.SubcontractProfileType);
            p.Add("@company_title_th_id", subcontractProfileCompany.CompanyTitleThId);
            p.Add("@company_title_en_id", subcontractProfileCompany.CompanyTitleEnId);
            p.Add("@status", subcontractProfileCompany.Status);
            p.Add("@activate_date", subcontractProfileCompany.ActivateDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompany_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string companyId)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", companyId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompany_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> subcontractProfileCompanyList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileCompanyDataTable(subcontractProfileCompanyList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompany_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileCompanyDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> SubcontractProfileCompanyList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("company_id", typeof(SqlGuid));
            dt.Columns.Add("company_code", typeof(SqlString));
            dt.Columns.Add("company_name", typeof(SqlString));
            dt.Columns.Add("company_name_th", typeof(SqlString));
            dt.Columns.Add("company_name_en", typeof(SqlString));
            dt.Columns.Add("company_alias", typeof(SqlString));
            dt.Columns.Add("distribution_channel", typeof(SqlString));
            dt.Columns.Add("channel_sale_group", typeof(SqlString));
            dt.Columns.Add("vendor_code", typeof(SqlString));
            dt.Columns.Add("customer_code", typeof(SqlString));
            dt.Columns.Add("area_id", typeof(SqlString));
            dt.Columns.Add("tax_id", typeof(SqlString));
            dt.Columns.Add("wt_name", typeof(SqlString));
            dt.Columns.Add("vat_type", typeof(SqlString));
            dt.Columns.Add("company_certified_file", typeof(SqlString));
            dt.Columns.Add("commercial_registration_file", typeof(SqlString));
            dt.Columns.Add("vat_registration_certificate_file", typeof(SqlString));
            dt.Columns.Add("contract_agreement_file", typeof(SqlString));
            dt.Columns.Add("deposit_authorization_level", typeof(SqlString));
            dt.Columns.Add("deposit_payment_type", typeof(SqlString));
            dt.Columns.Add("contract_start_date", typeof(SqlDateTime));
            dt.Columns.Add("contract_end_date", typeof(SqlDateTime));
            dt.Columns.Add("over_draft_deposit", typeof(SqlString));
            dt.Columns.Add("balance_deposit", typeof(SqlDecimal));
            dt.Columns.Add("company_status", typeof(SqlString));
            dt.Columns.Add("company_address", typeof(SqlString));
            dt.Columns.Add("vat_address", typeof(SqlString));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("update_by", typeof(SqlString));
            dt.Columns.Add("update_date", typeof(SqlDateTime));
            dt.Columns.Add("company_email", typeof(SqlString));
            dt.Columns.Add("contract_name", typeof(SqlString));
            dt.Columns.Add("contract_phone", typeof(SqlString));
            dt.Columns.Add("contract_email", typeof(SqlString));
            dt.Columns.Add("bank_code", typeof(SqlString));
            dt.Columns.Add("bank_name", typeof(SqlString));
            dt.Columns.Add("account_number", typeof(SqlString));
            dt.Columns.Add("account_name", typeof(SqlString));
            dt.Columns.Add("attach_file", typeof(SqlString));
            dt.Columns.Add("branch_code", typeof(SqlString));
            dt.Columns.Add("branch_name", typeof(SqlString));
            dt.Columns.Add("dept_of_install_name", typeof(SqlString));
            dt.Columns.Add("dept_of_mainten_name", typeof(SqlString));
            dt.Columns.Add("dept_of_account_name", typeof(SqlString));
            dt.Columns.Add("dept_of_install_phone", typeof(SqlString));
            dt.Columns.Add("dept_of_mainten_phone", typeof(SqlString));
            dt.Columns.Add("dept_of_account_phone", typeof(SqlString));
            dt.Columns.Add("dept_of_install_email", typeof(SqlString));
            dt.Columns.Add("dept_of_mainten_email", typeof(SqlString));
            dt.Columns.Add("dept_of_account_email", typeof(SqlString));
            dt.Columns.Add("location_code", typeof(SqlString));
            dt.Columns.Add("location_name_th", typeof(SqlString));
            dt.Columns.Add("location_name_en", typeof(SqlString));
            dt.Columns.Add("bank_account_type_id", typeof(SqlString));
            dt.Columns.Add("subcontract_profile_type", typeof(SqlString));
            dt.Columns.Add("company_title_th_id", typeof(SqlString));
            dt.Columns.Add("company_title_en_id", typeof(SqlString));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("activate_date", typeof(SqlDateTime));

            if (SubcontractProfileCompanyList != null)
                foreach (var curObj in SubcontractProfileCompanyList)
                {
                    DataRow row = dt.NewRow();
                    row["company_id"] = new SqlGuid(curObj.CompanyId);
                    row["company_code"] = new SqlString(curObj.CompanyCode);
                    row["company_name"] = new SqlString(curObj.CompanyName);
                    row["company_name_th"] = new SqlString(curObj.CompanyNameTh);
                    row["company_name_en"] = new SqlString(curObj.CompanyNameEn);
                    row["company_alias"] = new SqlString(curObj.CompanyAlias);
                    row["distribution_channel"] = new SqlString(curObj.DistributionChannel);
                    row["channel_sale_group"] = new SqlString(curObj.ChannelSaleGroup);
                    row["vendor_code"] = new SqlString(curObj.VendorCode);
                    row["customer_code"] = new SqlString(curObj.CustomerCode);
                    row["area_id"] = new SqlString(curObj.AreaId);
                    row["tax_id"] = new SqlString(curObj.TaxId);
                    row["wt_name"] = new SqlString(curObj.WtName);
                    row["vat_type"] = new SqlString(curObj.VatType);
                    row["company_certified_file"] = new SqlString(curObj.CompanyCertifiedFile);
                    row["commercial_registration_file"] = new SqlString(curObj.CommercialRegistrationFile);
                    row["vat_registration_certificate_file"] = new SqlString(curObj.VatRegistrationCertificateFile);
                    row["contract_agreement_file"] = new SqlString(curObj.ContractAgreementFile);
                    row["deposit_authorization_level"] = new SqlString(curObj.DepositAuthorizationLevel);
                    row["deposit_payment_type"] = new SqlString(curObj.DepositPaymentType);
                    row["contract_start_date"] = curObj.ContractStartDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ContractStartDate.Value);
                    row["contract_end_date"] = curObj.ContractEndDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ContractEndDate.Value);
                    row["over_draft_deposit"] = new SqlString(curObj.OverDraftDeposit);
                    row["balance_deposit"] = curObj.BalanceDeposit == null ? SqlDecimal.Null : new SqlDecimal(curObj.BalanceDeposit.Value);
                    row["company_status"] = new SqlString(curObj.CompanyStatus);
                    row["company_address"] = new SqlString(curObj.CompanyAddress);
                    row["vat_address"] = new SqlString(curObj.VatAddress);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["update_by"] = new SqlString(curObj.UpdateBy);
                    row["update_date"] = curObj.UpdateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.UpdateDate.Value);
                    row["company_email"] = new SqlString(curObj.CompanyEmail);
                    row["contract_name"] = new SqlString(curObj.ContractName);
                    row["contract_phone"] = new SqlString(curObj.ContractPhone);
                    row["contract_email"] = new SqlString(curObj.ContractEmail);
                    row["bank_code"] = new SqlString(curObj.BankCode);
                    row["bank_name"] = new SqlString(curObj.BankName);
                    row["account_number"] = new SqlString(curObj.AccountNumber);
                    row["account_name"] = new SqlString(curObj.AccountName);
                    row["attach_file"] = new SqlString(curObj.AttachFile);
                    row["branch_code"] = new SqlString(curObj.BranchCode);
                    row["branch_name"] = new SqlString(curObj.BranchName);
                    row["dept_of_install_name"] = new SqlString(curObj.DeptOfInstallName);
                    row["dept_of_mainten_name"] = new SqlString(curObj.DeptOfMaintenName);
                    row["dept_of_account_name"] = new SqlString(curObj.DeptOfAccountName);
                    row["dept_of_install_phone"] = new SqlString(curObj.DeptOfInstallPhone);
                    row["dept_of_mainten_phone"] = new SqlString(curObj.DeptOfMaintenPhone);
                    row["dept_of_account_phone"] = new SqlString(curObj.DeptOfAccountPhone);
                    row["dept_of_install_email"] = new SqlString(curObj.DeptOfInstallEmail);
                    row["dept_of_mainten_email"] = new SqlString(curObj.DeptOfMaintenEmail);
                    row["dept_of_account_email"] = new SqlString(curObj.DeptOfAccountEmail);
                    row["location_code"] = new SqlString(curObj.LocationCode);
                    row["location_name_th"] = new SqlString(curObj.LocationNameTh);
                    row["location_name_en"] = new SqlString(curObj.LocationNameEn);
                    row["bank_account_type_id"] = new SqlString(curObj.BankAccountTypeId);
                    row["subcontract_profile_type"] = new SqlString(curObj.SubcontractProfileType);
                    row["company_title_th_id"] = new SqlString(curObj.CompanyTitleThId);
                    row["company_title_en_id"] = new SqlString(curObj.CompanyTitleEnId);
                    row["status"] = new SqlString(curObj.Status);
                    row["activate_date"] = curObj.ActivateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ActivateDate.Value);
                   
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileCompanyPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>
                ("uspSubcontractProfileCompany_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileCompanyPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("company_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["company_id"] = new SqlGuid(curObj.CompanyId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchCompany(string subcontract_profile_type, 
            string location_code, string vendor_code, string company_th,
            string company_en, string company_alias, string company_code, 
            string distibution_channel, string channel_sale_group)
        {
            var p = new DynamicParameters();
            p.Add("@subcontract_profile_type", subcontract_profile_type);
            p.Add("@location_code", location_code);
            p.Add("@vendor_code", vendor_code);
            p.Add("@company_th", company_th);
            p.Add("@company_en", company_en);
            p.Add("@company_alias", company_alias);
            p.Add("@company_code", company_code);
            p.Add("@distibution_channel", distibution_channel);
            p.Add("@channel_sale_group", channel_sale_group);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>
            ("uspSubcontractProfileCompany_Search", p, commandType: CommandType.StoredProcedure);

            return entity;

          
        }
    }
}
