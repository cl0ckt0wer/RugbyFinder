using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    [ApiController]
    [Route("[controller]")]
    public class TeamOwnerController
    {
        private SqlConnectionStringBuilder _cstring;

        public TeamOwnerController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet]
        [Route("{myguid:guid}")]
        public async Task<TeamModel> GetOwnedTeamAsync(Guid myguid)
        {
            var sql = "Proc_GetTeamByOwner";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var x = await conn.QueryAsync<TeamPostModel, CityInfo, TeamPostModel>(sql
                    , (teamModel, cityInfo) =>
                    {
                        teamModel.City = cityInfo;
                        return teamModel;
                    }, splitOn: "CityId"
                    , param: new { ownerGuid = myguid }, commandType: CommandType.StoredProcedure);
                return x.FirstOrDefault();
            }
        }

    }
}
