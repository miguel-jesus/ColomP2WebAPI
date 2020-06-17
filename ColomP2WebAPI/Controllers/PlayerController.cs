using ColomP2WebAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using MySql;

namespace ColomP2WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Player")]
    public class PlayerController : ApiController
    {
        [HttpGet]
        [Route("GetPlayerInfo")]
        public PlayerModel GetPlayerInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection){
                string sql = $"SELECT Id,Name,DateBirth FROM dbo.Player WHERE Id LIKE '{authenticatedAspNetUserId}'";

                var player = cnn.Query<PlayerModel>(sql).FirstOrDefault();
                return player;
            }

              
        }

        [HttpPost]
        [Route("InsertPlayer")]
        public bool InsertPlayer(PlayerModel player)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"INSERT INTO dbo.Player(Id,Name,DateBirth) VALUES('{player.Id}','{player.Name}','{player.DateBirth}')";

                int rows = cnn.Execute(sql);
                if (rows != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }


        }

    }
}
