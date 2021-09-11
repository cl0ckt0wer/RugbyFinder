using RugbyFinder.Shared.Model.Return;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Threading.Tasks;

namespace RugbyFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyGenController : Controller
    {
        private SqlConnectionStringBuilder _cstring;
        public KeyGenController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet]
        public async Task<KeyGenReturnModel> GetNewKeyAsync()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
          
          
            var sql = "Proc_RegisterNewKey";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
               
                    var keyGenReturnModel = await conn.QueryFirstAsync<KeyGenReturnModel>(sql, new { apiKey  }
                        , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return keyGenReturnModel;
            }
        }
    }
}
