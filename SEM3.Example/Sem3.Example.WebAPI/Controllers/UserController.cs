using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sem3.Example.WebAPI.Models;

namespace Sem3.Example.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet(Name = "GetUsers")]
        public List<UserModel> GetUsers()
        {
            return new List<UserModel>()
            {
                new UserModel()
                {
                    UserId ="1",
                    UserName ="Anhnv",
                    Role ="Admin"
                },
                  new UserModel()
                {
                    UserId ="2",
                    UserName ="hangtt",
                    Role ="CS"
                },
            };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] UserModel model)
        {
            Console.WriteLine(model.UserName);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
