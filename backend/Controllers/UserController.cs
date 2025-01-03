using System.Security.Cryptography;
using System.Text;
using backend.Data;
using backend.DTOs;
using backend.interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Authorize]
    public class UserController : BaseController<User>
    {
        private readonly DataBase dataBase;
        private readonly ITokenService tokenService;

        public UserController(DataBase dataBase, ITokenService tokenService) : base(dataBase)
        {
            this.dataBase = dataBase;
            this.tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {

            return Ok(dataBase.Users);
        }

        [HttpGet("user/{id}")]
        public ActionResult<IEnumerable<User>> GetUser(int id)
        {
            var user = dataBase.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return Content($"User with id {id} not found");
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> AddUser(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();
            if(dataBase.Users.Any(user => user.Username == registerDTO.Username))
            {
                return Content($"{registerDTO.Username} already exists"); 
            }
            var user = new User(){
                Username = registerDTO.Username,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordKey = hmac.Key
            };
            dataBase.Users.Add(user);
            await dataBase.SaveChangesAsync();
            return Ok(new UserDTO(){
                Username = registerDTO.Username,
                Token = tokenService.CreateToken(user)
            });
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserDTO> Login(LoginDTO loginDTO)
        {
            var user = dataBase.Users.FirstOrDefault(user => user.Username == loginDTO.username);
            if(user == null)
            {
                return Content($"User {loginDTO.username} not found");
            }
            Console.WriteLine(user);
            using var hmac = new HMACSHA512(user.PasswordKey);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));
            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.Password[i])
                {
                    return Content("Invalid password");
                }
            }
            return Ok(new UserDTO(){
                Username = loginDTO.username,
                Token = tokenService.CreateToken(user)
            });
        }
        

        [HttpGet("remove")]
        public ActionResult<IEnumerable<User>> RemoveUser(int id)
        {
            var user = dataBase.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return Content($"User with id {id} not found");
            }
            dataBase.Users.Remove(user);
            dataBase.SaveChanges();

            return Ok(dataBase.Users);
        }


        

    }
}
