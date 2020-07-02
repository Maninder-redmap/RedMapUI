using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Redmap.Events.Repository
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public class BaseRepository
    {

        #region database connection
        private string connectionString;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
        #endregion

        /// <summary>
        /// Generic Get Function.    
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        
        
        /// <returns></returns>
        public IEnumerable<T> Get<T>(string sql)
        {
            IEnumerable<T> result = default(IEnumerable<T>);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                result = dbConnection.Query<T>(sql);                
            }

            return result;
        }

        /// <summary>
        /// Add Function.       
        /// </summary>
        
        /// <param name="sql"></param>
        /// <param name="parameters"></param>

        /// <returns></returns>
        public int Add(string sql, object parameters = null)
        {
            int result = default(int);

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                result = dbConnection.Query<int>(sql,parameters).FirstOrDefault();
                return result;
            }            
        }
    }
}
