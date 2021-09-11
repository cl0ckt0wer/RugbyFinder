using RugbyFinder.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuggerInboxController : Controller
    {
      
        private SqlConnectionStringBuilder _cstring;
      
        public RuggerInboxController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
       
        [HttpGet("{key}")]
        public async Task<IEnumerable<InboxItem>> GetInboxAsync(string key)
        {
            key = key.Replace("%2f", "%2F").Replace("%2F", "/");

            var sql = "Proc_GetInbox";
            using(var conn = new SqlConnection(_cstring.ConnectionString))
            {
                IEnumerable<InboxItem> r = await conn.QueryAsync<InboxItem>(sql, new { key }, commandType: System.Data.CommandType.StoredProcedure);
                return r ;
            }
        }


    }
}
