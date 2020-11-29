using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
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
        [HttpGet]
        [Route("{myguid:guid}")]
        public async Task<MyInfo> NameAsync(Guid myguid)
        {
            var ret = new MyInfo();
            var dparams = new DynamicParameters();
            var mynameparam = "";
            var mybio = "";
            var sql = "Proc_GetMyInfo";
            dparams.Add("uid", myguid);
            dparams.Add("name", mynameparam, DbType.String, ParameterDirection.Output, 50);
            dparams.Add("bio", mybio, DbType.String, ParameterDirection.Output, size: int.MaxValue);
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                await conn.ExecuteAsync(sql, param: dparams, commandType: CommandType.StoredProcedure);
            }
            ret.MyName = dparams.Get<string>("name");
            ret.MyBio = dparams.Get<string>("bio");
            return ret;

        }
        [HttpPost]
        public async Task NameAsync(UpdateRuggerArgs args)
        {
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var sql = "Proc_UpsertName";
                await conn.ExecuteAsync(sql, new { Id = args.MyGuid, name = args.MyName, bio = args.Bio }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
