using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;

using System.IO;

/// =================================================================
/// Author: kessada x10
/// Description : DB Context interface and class
/// =================================================================
namespace SubContractProfile.Infrastructure {
  
    
    public interface IDbContext : IDisposable 
    {

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        Task<IDbTransaction> OpenTransaction();
        Task<IDbTransaction> OpenTransaction(IsolationLevel level);
        void CommitTransaction(bool disposeTrans = true);
        void RollbackTransaction(bool disposeTrans = true);


        SubContractProfile.Infrastructure.ISubcontractProfileCompanyRepo SubcontractProfileCompanyRepo { get; }
        SubContractProfile.Infrastructure.ISubcontractProfileEngineerRepo SubcontractProfileEngineerRepo { get; }
        SubContractProfile.Infrastructure.ISubcontractProfileLocationRepo SubcontractProfileLocationRepo { get; }
        SubContractProfile.Infrastructure.ISubcontractProfilePersonalRepo SubcontractProfilePersonalRepo { get; }
        SubContractProfile.Infrastructure.ISubcontractProfileTeamRepo SubcontractProfileTeamRepo { get; }

    }

    public class DbContext : IDbContext
    {

        protected readonly IConfiguration _config;

        
        protected IDbConnection _cn = null;
        public IDbConnection Connection {
            get => _cn;
        }

        
        protected IDbTransaction _trans = null;
        public IDbTransaction Transaction { 
            get => _trans;
        }


        
        protected SubContractProfile.Infrastructure.ISubcontractProfileCompanyRepo _subcontractProfileCompanyRepo;
        public SubContractProfile.Infrastructure.ISubcontractProfileCompanyRepo SubcontractProfileCompanyRepo {
            get {
                if (_subcontractProfileCompanyRepo == null) 
                    _subcontractProfileCompanyRepo = new SubContractProfile.Infrastructure.SubcontractProfileCompanyRepo(this);
                return _subcontractProfileCompanyRepo;
            }
        }


        protected SubContractProfile.Infrastructure.ISubcontractProfileEngineerRepo _subcontractProfileEngineerRepo;
        public SubContractProfile.Infrastructure.ISubcontractProfileEngineerRepo SubcontractProfileEngineerRepo {
            get {
                if (_subcontractProfileEngineerRepo == null) 
                    _subcontractProfileEngineerRepo = new SubContractProfile.Infrastructure.SubcontractProfileEngineerRepo(this);
                return _subcontractProfileEngineerRepo;
            }
        }


        protected SubContractProfile.Infrastructure.ISubcontractProfileLocationRepo _subcontractProfileLocationRepo;
        public SubContractProfile.Infrastructure.ISubcontractProfileLocationRepo SubcontractProfileLocationRepo {
            get {
                if (_subcontractProfileLocationRepo == null) 
                    _subcontractProfileLocationRepo = new SubContractProfile.Infrastructure.SubcontractProfileLocationRepo(this);
                return _subcontractProfileLocationRepo;
            }
        }


        protected SubContractProfile.Infrastructure.ISubcontractProfilePersonalRepo _subcontractProfilePersonalRepo;
        public SubContractProfile.Infrastructure.ISubcontractProfilePersonalRepo SubcontractProfilePersonalRepo {
            get {
                if (_subcontractProfilePersonalRepo == null) 
                    _subcontractProfilePersonalRepo = new SubContractProfile.Infrastructure.SubcontractProfilePersonalRepo(this);
                return _subcontractProfilePersonalRepo;
            }
        }


        protected SubContractProfile.Infrastructure.ISubcontractProfileTeamRepo _subcontractProfileTeamRepo;
        public SubContractProfile.Infrastructure.ISubcontractProfileTeamRepo SubcontractProfileTeamRepo {
            get {
                if (_subcontractProfileTeamRepo == null) 
                    _subcontractProfileTeamRepo = new SubContractProfile.Infrastructure.SubcontractProfileTeamRepo(this);
                return _subcontractProfileTeamRepo;
            }
        }



        /// <summary>
        /// Main constructor, inject standard config : Default connection string
        /// Need to be reviewed to be more generic (choose the connection string to inject)
        /// </summary>
        public DbContext(IConfiguration config)
        {
            _config = config;
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _config = builder.Build();

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _cn = new SqlConnection(_config.GetConnectionString("Default"));


        }
        

        /// <summary>
        /// Open a transaction
        /// </summary>
        public async Task<IDbTransaction> OpenTransaction()
        {
            if(_trans != null)
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
            if(_trans != null)
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
            if  (_trans == null)
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
            if  (_trans == null)
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


