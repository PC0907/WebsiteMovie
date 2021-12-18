using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DotNetAssignment.Models;
using DotNetAssignment.Services;
using DotNetAssignment.Database;
using System.Data.SQLite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace DotNetAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet("add")]
        public LocalRedirectResult AddMovie([FromQuery] string id) 
        {

            DatabaseCon con = new DatabaseCon();
            try
            {
                string movie_id = Convert.ToString(id);
                con.OpenConnection();
                string mail = HttpContext.Session.GetString("mail");
                Console.WriteLine(mail);
                string query = "INSERT INTO playlist ('movie_id', 'mail') VALUES(@movie_id, @mail)";

                SQLiteCommand comm = new SQLiteCommand(query, con.myConnection);
                comm.Parameters.AddWithValue("@movie_id", movie_id);
                comm.Parameters.AddWithValue("@mail", mail);
                var res = comm.ExecuteNonQuery();
            }
            catch (Exception) {
               Console.WriteLine("Error occurred while adding to playlist");
            }
            finally
            {
                con.CloseConnetion();
            }
           return LocalRedirect("/");
           
        }

        [Authorize]
        [HttpGet("getMovies")]
        public List<int> GetMovies()
        {
            string mail = HttpContext.Session.GetString("mail");
            List<int> Ids =  new List<int>();
            DatabaseCon con = new DatabaseCon();
            try
            {
                con.OpenConnection();
                string query = "SELECT movie_id FROM playlist WHERE mail=@mail";
                SQLiteCommand myCommand = new SQLiteCommand(query, con.myConnection);
                myCommand.Parameters.AddWithValue("@mail", mail);
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {

                    while (result.Read())
                    {
                        int id = Convert.ToInt32(result["movie_id"]);
                        Ids.Add(id);
                    }
                }
            }

            catch (Exception)
            {
                Console.WriteLine("Cannot fetch movies");
            }
            finally
            {
                con.CloseConnetion();
            }

            Console.WriteLine(Ids);
            return Ids;
        }

    }
}
