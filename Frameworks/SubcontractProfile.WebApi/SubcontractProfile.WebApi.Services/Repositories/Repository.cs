using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;

/// =================================================================
/// Author: AIS Fibre
/// Description : DB Context interface and class
/// =================================================================
namespace Repository
{

    public interface IDbContext : IDisposable
    {

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        Task<IDbTransaction> OpenTransaction();
        Task<IDbTransaction> OpenTransaction(IsolationLevel level);
        void CommitTransaction(bool disposeTrans = true);
        void RollbackTransaction(bool disposeTrans = true);


        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileAddressRepo SubcontractProfileAddressRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileAsstEngineerRepo SubcontractProfileAsstEngineerRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileBankingRepo SubcontractProfileBankingRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileCompanyRepo SubcontractProfileCompanyRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileDistrictRepo SubcontractProfileDistrictRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileEngineerRepo SubcontractProfileEngineerRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileRegionRepo SubcontractProfileRegionRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileLocationRepo SubcontractProfileLocationRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileNationalityRepo SubcontractProfileNationalityRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfilePaymentRepo SubcontractProfilePaymentRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfilePersonalRepo SubcontractProfilePersonalRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileProvinceRepo SubcontractProfileProvinceRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileRaceRepo SubcontractProfileRaceRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileReligionRepo SubcontractProfileReligionRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileSubDistrictRepo SubcontractProfileSubDistrictRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTeamRepo SubcontractProfileTeamRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTitleRepo SubcontractProfileTitleRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTrainingRepo SubcontractProfileTrainingRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileUserRepo SubcontractProfileUserRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleBrandRepo SubcontractProfileVerhicleBrandRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleSeriseRepo SubcontractProfileVerhicleSeriseRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleTypeRepo SubcontractProfileVerhicleTypeRepo { get; }
        SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileWarrantyRepo SubcontractProfileWarrantyRepo { get; }

    }


    public class DbContext : IDbContext
    {


        protected readonly IConfiguration _config;


        protected IDbConnection _cn = null;
        public IDbConnection Connection
        {
            get => _cn;
        }


        protected IDbTransaction _trans = null;
        public IDbTransaction Transaction
        {
            get => _trans;
        }



        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileAddressRepo _subcontractProfileAddressRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileAddressRepo SubcontractProfileAddressRepo
        {
            get
            {
                if (_subcontractProfileAddressRepo == null)
                    _subcontractProfileAddressRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileAddressRepo(this);
                return _subcontractProfileAddressRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileAsstEngineerRepo _subcontractProfileAsstEngineerRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileAsstEngineerRepo SubcontractProfileAsstEngineerRepo
        {
            get
            {
                if (_subcontractProfileAsstEngineerRepo == null)
                    _subcontractProfileAsstEngineerRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileAsstEngineerRepo(this);
                return _subcontractProfileAsstEngineerRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileBankingRepo _subcontractProfileBankingRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileBankingRepo SubcontractProfileBankingRepo
        {
            get
            {
                if (_subcontractProfileBankingRepo == null)
                    _subcontractProfileBankingRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileBankingRepo(this);
                return _subcontractProfileBankingRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileCompanyRepo _subcontractProfileCompanyRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileCompanyRepo SubcontractProfileCompanyRepo
        {
            get
            {
                if (_subcontractProfileCompanyRepo == null)
                    _subcontractProfileCompanyRepo = new SubcontractProfile.WebApi.Services.SubcontractProfileCompanyRepo(this);
                return _subcontractProfileCompanyRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileDistrictRepo _subcontractProfileDistrictRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileDistrictRepo SubcontractProfileDistrictRepo
        {
            get
            {
                if (_subcontractProfileDistrictRepo == null)
                    _subcontractProfileDistrictRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileDistrictRepo(this);
                return _subcontractProfileDistrictRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileEngineerRepo _subcontractProfileEngineerRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileEngineerRepo SubcontractProfileEngineerRepo
        {
            get
            {
                if (_subcontractProfileEngineerRepo == null)
                    _subcontractProfileEngineerRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileEngineerRepo(this);
                return _subcontractProfileEngineerRepo;
            }
        }

        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileRegionRepo _subcontractProfileRegionRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileRegionRepo SubcontractProfileRegionRepo
        {
            get
            {
                if (_subcontractProfileRegionRepo == null)
                    _subcontractProfileRegionRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileRegionRepo(this);
                return _subcontractProfileRegionRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileLocationRepo _subcontractProfileLocationRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileLocationRepo SubcontractProfileLocationRepo
        {
            get
            {
                if (_subcontractProfileLocationRepo == null)
                    _subcontractProfileLocationRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileLocationRepo(this);
                return _subcontractProfileLocationRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileNationalityRepo _subcontractProfileNationalityRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileNationalityRepo SubcontractProfileNationalityRepo
        {
            get
            {
                if (_subcontractProfileNationalityRepo == null)
                    _subcontractProfileNationalityRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileNationalityRepo(this);
                return _subcontractProfileNationalityRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfilePaymentRepo _subcontractProfilePaymentRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfilePaymentRepo SubcontractProfilePaymentRepo
        {
            get
            {
                if (_subcontractProfilePaymentRepo == null)
                    _subcontractProfilePaymentRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfilePaymentRepo(this);
                return _subcontractProfilePaymentRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfilePersonalRepo _subcontractProfilePersonalRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfilePersonalRepo SubcontractProfilePersonalRepo
        {
            get
            {
                if (_subcontractProfilePersonalRepo == null)
                    _subcontractProfilePersonalRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfilePersonalRepo(this);
                return _subcontractProfilePersonalRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileProvinceRepo _subcontractProfileProvinceRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileProvinceRepo SubcontractProfileProvinceRepo
        {
            get
            {
                if (_subcontractProfileProvinceRepo == null)
                    _subcontractProfileProvinceRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileProvinceRepo(this);
                return _subcontractProfileProvinceRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileRaceRepo _subcontractProfileRaceRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileRaceRepo SubcontractProfileRaceRepo
        {
            get
            {
                if (_subcontractProfileRaceRepo == null)
                    _subcontractProfileRaceRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileRaceRepo(this);
                return _subcontractProfileRaceRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileReligionRepo _subcontractProfileReligionRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileReligionRepo SubcontractProfileReligionRepo
        {
            get
            {
                if (_subcontractProfileReligionRepo == null)
                    _subcontractProfileReligionRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileReligionRepo(this);
                return _subcontractProfileReligionRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileSubDistrictRepo _subcontractProfileSubDistrictRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileSubDistrictRepo SubcontractProfileSubDistrictRepo
        {
            get
            {
                if (_subcontractProfileSubDistrictRepo == null)
                    _subcontractProfileSubDistrictRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileSubDistrictRepo(this);
                return _subcontractProfileSubDistrictRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTeamRepo _subcontractProfileTeamRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTeamRepo SubcontractProfileTeamRepo
        {
            get
            {
                if (_subcontractProfileTeamRepo == null)
                    _subcontractProfileTeamRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileTeamRepo(this);
                return _subcontractProfileTeamRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTitleRepo _subcontractProfileTitleRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTitleRepo SubcontractProfileTitleRepo
        {
            get
            {
                if (_subcontractProfileTitleRepo == null)
                    _subcontractProfileTitleRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileTitleRepo(this);
                return _subcontractProfileTitleRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTrainingRepo _subcontractProfileTrainingRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileTrainingRepo SubcontractProfileTrainingRepo
        {
            get
            {
                if (_subcontractProfileTrainingRepo == null)
                    _subcontractProfileTrainingRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileTrainingRepo(this);
                return _subcontractProfileTrainingRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileUserRepo _subcontractProfileUserRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileUserRepo SubcontractProfileUserRepo
        {
            get
            {
                if (_subcontractProfileUserRepo == null)
                    _subcontractProfileUserRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileUserRepo(this);
                return _subcontractProfileUserRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleBrandRepo _subcontractProfileVerhicleBrandRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleBrandRepo SubcontractProfileVerhicleBrandRepo
        {
            get
            {
                if (_subcontractProfileVerhicleBrandRepo == null)
                    _subcontractProfileVerhicleBrandRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileVerhicleBrandRepo(this);
                return _subcontractProfileVerhicleBrandRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleSeriseRepo _subcontractProfileVerhicleSeriseRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleSeriseRepo SubcontractProfileVerhicleSeriseRepo
        {
            get
            {
                if (_subcontractProfileVerhicleSeriseRepo == null)
                    _subcontractProfileVerhicleSeriseRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileVerhicleSeriseRepo(this);
                return _subcontractProfileVerhicleSeriseRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleTypeRepo _subcontractProfileVerhicleTypeRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileVerhicleTypeRepo SubcontractProfileVerhicleTypeRepo
        {
            get
            {
                if (_subcontractProfileVerhicleTypeRepo == null)
                    _subcontractProfileVerhicleTypeRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileVerhicleTypeRepo(this);
                return _subcontractProfileVerhicleTypeRepo;
            }
        }


        protected SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileWarrantyRepo _subcontractProfileWarrantyRepo;
        public SubcontractProfile.WebApi.Services.Contracts.ISubcontractProfileWarrantyRepo SubcontractProfileWarrantyRepo
        {
            get
            {
                if (_subcontractProfileWarrantyRepo == null)
                    _subcontractProfileWarrantyRepo = new SubcontractProfile.WebApi.Services.Services.SubcontractProfileWarrantyRepo(this);
                return _subcontractProfileWarrantyRepo;
            }
        }



        /// <summary>
        /// Main constructor, inject standard config : Default connection string
        /// Need to be reviewed to be more generic (choose the connection string to inject)
        /// </summary>
        public DbContext(IConfiguration config)
        {
            _config = config;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _cn = new SqlConnection(_config.GetConnectionString("Default"));
        }


        /// <summary>
        /// Open a transaction
        /// </summary>
        public async Task<IDbTransaction> OpenTransaction()
        {
            if (_trans != null)
                throw new Exception("A transaction is already open, you need to use a new DbContext for parallel job.");

            if (_cn.State == ConnectionState.Closed)
            {
                if (!(_cn is DbConnection))
                    throw new Exception("Connection object does not support OpenAsync.");

                await (_cn as DbConnection).OpenAsync();
            }

            _trans = _cn.BeginTransaction();

            return _trans;
        }


        /// <summary>
        /// Open a transaction with a specified isolation level
        /// </summary>
        public async Task<IDbTransaction> OpenTransaction(IsolationLevel level)
        {
            if (_trans != null)
                throw new Exception("A transaction is already open, you need to use a new DbContext for parallel job.");

            if (_cn.State == ConnectionState.Closed)
            {
                if (!(_cn is DbConnection))
                    throw new Exception("Connection object does not support OpenAsync.");

                await (_cn as DbConnection).OpenAsync();
            }

            _trans = _cn.BeginTransaction(level);

            return _trans;
        }


        /// <summary>
        /// Commit the current transaction, and optionally dispose all resources related to the transaction.
        /// </summary>
        public void CommitTransaction(bool disposeTrans = true)
        {
            if (_trans == null)
                throw new Exception("DB Transaction is not present.");

            _trans.Commit();
            if (disposeTrans) _trans.Dispose();
            if (disposeTrans) _trans = null;
        }

        /// <summary>
        /// Rollback the transaction and all the operations linked to it, and optionally dispose all resources related to the transaction.
        /// </summary>
        public void RollbackTransaction(bool disposeTrans = true)
        {
            if (_trans == null)
                throw new Exception("DB Transaction is not present.");

            _trans.Rollback();
            if (disposeTrans) _trans.Dispose();
            if (disposeTrans) _trans = null;
        }

        /// <summary>
        /// Will be call at the end of the service (ex : transient service in api net core)
        /// </summary>
        public void Dispose()
        {
            _trans?.Dispose();
            _cn?.Close();
            _cn?.Dispose();
        }


    }



}

