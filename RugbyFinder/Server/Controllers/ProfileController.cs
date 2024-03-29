﻿using RugbyFinder.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace RugbyFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public ProfileController(IConfiguration configuration)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("{myguid:guid}")]
        public async Task<ProfileInfo> GetProfileAsync(Guid myguid)
        {
            var sql = "Proc_GetProfileInfo";
            using (SqlConnection cnn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var profile = await cnn.QueryFirstOrDefaultAsync<ProfileInfo>(sql, new { guid = myguid }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return profile;
            }
        }
    }
}
