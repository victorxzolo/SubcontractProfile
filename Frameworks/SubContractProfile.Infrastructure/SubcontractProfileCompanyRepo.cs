using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SubContractProfile.Infrastructure
{


    /// =================================================================
    /// Author: kessada x10
    /// Description:	Interface for the repo ISubcontractProfileCompanyRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileCompanyRepo
    {

        Task<IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany>> GetAll();
        Task<SubcontractProfile.Core.Entities.SubcontractProfileCompany> GetByCompanyId(System.Guid companyId);
        Task<bool> Insert(SubcontractProfile.Core.Entities.SubcontractProfileCompany subcontractProfileCompany);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany> subcontractProfileCompanyList);
        Task<bool> Update(SubcontractProfile.Core.Entities.SubcontractProfileCompany subcontractProfileCompany);
        Task<bool> Delete(System.Guid id);
        Task<IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany>> GetByPKList(IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany_PK> pkList);

    }




    /// =================================================================
    /// Author: kessada x10
    /// Description:	Class for the repo SubcontractProfileCompanyRepo 
    /// =================================================================
    public partial class SubcontractProfileCompanyRepo : ISubcontractProfileCompanyRepo
    {

        protected SubContractProfile.Infrastructure.DbContext _dbContext = null;

        public SubcontractProfileCompanyRepo(SubContractProfile.Infrastructure.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.Core.Entities.SubcontractProfileCompany>
            ("uspSubcontractProfileCompany_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.Core.Entities.SubcontractProfileCompany> GetByCompanyId(System.Guid companyId)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", companyId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.Core.Entities.SubcontractProfileCompany>
            ("uspSubcontractProfileCompany_selectByCompanyId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.Core.Entities.SubcontractProfileCompany subcontractProfileCompany)
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

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompany_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.Core.Entities.SubcontractProfileCompany subcontractProfileCompany)
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

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompany_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(System.Guid companyId)
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
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany> subcontractProfileCompanyList)
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
        private object CreateSubcontractProfileCompanyDataTable(IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany> SubcontractProfileCompanyList)
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

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany>> GetByPKList(IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileCompanyPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.Core.Entities.SubcontractProfileCompany>
                ("uspSubcontractProfileCompany_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileCompanyPKDataTable(IEnumerable<SubcontractProfile.Core.Entities.SubcontractProfileCompany_PK> pkList)
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

    }



}

