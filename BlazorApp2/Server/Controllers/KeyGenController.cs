using BlazorApp2.Shared.Model.Return;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
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
          
            apiKey = HtmlEncoder.Default.Encode(apiKey);
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
