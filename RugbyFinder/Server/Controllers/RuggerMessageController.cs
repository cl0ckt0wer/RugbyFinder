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
    public class RuggerMessageController : Controller
    {
        private SqlConnectionStringBuilder _cstring;

        public RuggerMessageController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet("{key}/{theirid:guid}")]
        public async Task<IEnumerable<RuggerMessageModel>> GetRuggerMessageAsync(string key, Guid theirid)
        {
            key = key.Replace("%2f", "%2F").Replace("%2F", "/");

            var sql = "Proc_GetRuggerMessage";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var result = await conn.QueryAsync<RuggerMessageModel>(sql, new { key, theirid }, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
                
        }
        [HttpPost]
        public async Task<IActionResult> PostRuggerMessage(RuggerMessagePostArgs args)
        {
            if (args.Message.Length > 2000)
            {
                args.Message = args.Message.Substring(0, 2000) + "MESSAGE TRUNCATED (MAX LENGTH 2000 CHARACTERS)";
            }
            var sql = "Proc_InsertRuggerMessage";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                await conn.ExecuteAsync(sql, new { Key = args.MyKey, To = args.TheirGuid, Message = args.Message }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return Accepted();
        }
    }
}
