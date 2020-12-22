using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        [HttpGet]
        [Route("{myguid:guid}")]
        public async Task<MyInfo> NameAsync(Guid myguid)
        {
            var sql = "Proc_GetMyInfo";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                //myInfo, TeamModel, CityInfo
                var y = await conn.QueryAsync<MyInfo, TeamModel, CityInfo, MyInfo>(sql, (info, team, city) =>
                {
                    info.MyTeam = team ?? new TeamModel();
                    info.MyTeam.City = city ?? new CityInfo();
                    return info;
                }, splitOn: "TeamId, CityId", param: new { uid = myguid }, commandType: CommandType.StoredProcedure);
                //var x = await conn.QueryFirstOrDefaultAsync<MyInfo>(sql, new { uid = myguid } , commandType: CommandType.StoredProcedure);
                return y.FirstOrDefault() ?? new MyInfo();
            }
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
