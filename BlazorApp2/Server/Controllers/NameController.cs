using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;
        public NameController(IConfiguration configuration)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task NameAsync(UpdateNameArgs args)
        {
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var sql = "Proc_UpsertName";
                await conn.ExecuteAsync(sql, new { Id = args.MyGuid, name = args.MyName }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
