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
        [HttpGet("{myid:guid}/{theirid:guid}")]
        public async Task<IEnumerable<RuggerMessageModel>> GetRuggerMessageAsync(Guid myid, Guid theirid)
        {
            var sql = "Proc_GetRuggerMessage";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var result = await conn.QueryAsync<RuggerMessageModel>(sql, new { myid, theirid }, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
                
        }
        [HttpPost]
        public async Task<IActionResult> PostRuggerMessage(RuggerMessagePostArgs args)
        {
            if (args.Message.Length > 2000)
            {
                args.Message = args.Message.Substring(0, 2000);
            }
            var sql = "Proc_InsertRuggerMessage";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                await conn.ExecuteAsync(sql, new { From = args.MyGuid, To = args.TheirGuid, Message = args.Message }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return Accepted();
        }
    }
}
