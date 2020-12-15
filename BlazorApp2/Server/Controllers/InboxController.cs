using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InboxController : Controller
    {
      
        private SqlConnectionStringBuilder _cstring;
      
        public InboxController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet]
        [Route("{myguid:guid}")]
        public async Task<IEnumerable<InboxItem>> GetInboxAsync(Guid myguid)
        {
            var sql = "Proc_GetInbox";
            using(var conn = new SqlConnection(_cstring.ConnectionString))
            {
                IEnumerable<InboxItem> r = await conn.QueryAsync<InboxItem>(sql, new { id  = myguid }, commandType: System.Data.CommandType.StoredProcedure);
                return r ;
            }
        }


    }
}
