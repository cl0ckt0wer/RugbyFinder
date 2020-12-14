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
    public class RuggerMessageController : Controller
    {
        private SqlConnectionStringBuilder _cstring;

        public RuggerMessageController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet("{myid:guid/theirid:guid")]
        public async Task<IEnumerable<RuggerMessageModel>> GetRuggerMessageAsync(Guid myid, Guid theirid)
        {
            var sql = "Proc_GetRuggerMessage";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var result = await conn.QueryAsync<RuggerMessageModel>(sql, new { myid, theirid }, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
                
        }
    }
}
