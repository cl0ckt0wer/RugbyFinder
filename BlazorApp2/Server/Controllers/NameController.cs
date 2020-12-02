using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            //    var ret = new MyInfo();
            //    var dparams = new DynamicParameters();
            //    var mynameparam = "";
            //    var mybio = "";
            //    var teamname = "";
            //    var teamguid = Guid.Empty;
            //    var profilepic = new List<byte>();
            var sql = "Proc_GetMyInfo";
            
            //dparams.Add("uid", myguid);
            //dparams.Add("name", mynameparam, DbType.String, ParameterDirection.Output, 50);
            //dparams.Add("bio", mybio, DbType.String, ParameterDirection.Output, size: int.MaxValue);
            //dparams.Add("TeamName", teamname, DbType.String, ParameterDirection.Output, size: 200);
            //dparams.Add("TeamId", teamguid, DbType.Guid, ParameterDirection.Output);
            //dparams.Add("ProfilePic", profilepic, DbType.Binary, ParameterDirection.Output);

            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var x = await conn.QueryFirstOrDefaultAsync<MyInfo>(sql, new { uid = myguid } , commandType: CommandType.StoredProcedure);
                return x ?? new MyInfo();
            }
            //ret.MyName = dparams.Get<string>("name");
            //ret.MyBio = dparams.Get<string>("bio");
            //ret.TeamName = dparams.Get<string>("TeamName");
            //uniqueIdentfiers always come back as nullable guids i guess?
            //https://stackoverflow.com/questions/20685800/using-dapper-dot-net-how-do-i-map-sql-uniqueidentifier-column-to-a-net-type
            //ret.TeamId = dparams.Get<Guid?>("TeamId");
            //ret.ProfilePic = dparams.Get<List<byte>>("ProfilePic").ToArray();
            //profilepic = dparams.Get<MemoryStream>("ProfilePic");
            //ret.ProfilePic = (profilepic ?? new MemoryStream()).ToArray() ;
            //return ret;

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
