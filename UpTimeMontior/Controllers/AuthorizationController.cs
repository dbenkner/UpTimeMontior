using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using UpTimeMontior.Data;
using UpTimeMontior.DTOs;
using UpTimeMontior.Models;

namespace UpTimeMontior.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly UpTimeDBContext _context;

        public AuthorizationController(UpTimeDBContext context) 
        {
            _context = context;
        }


        [HttpPost("NewUser")]
        public async Task<ActionResult<User>> RegisterUser(NewUserDTO newUser)
        {
            if(await UserNameExists(newUser.UserName))
            {
                return BadRequest("Username Already Exists!");
            }
            if(await EmailExists(newUser.Email))
            {
                return BadRequest("Email Already Exists!");
            }
            using var hmac = new HMACSHA512();
            User user = new User()
            {
                UserName = newUser.UserName,
                UserEmail = newUser.Email,
                DateCreated = DateTime.Now,
                PasswordExpires = DateTime.Now.AddDays(180),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newUser.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost("SignIn")]
        public async Task<ActionResult<User>> SignInUser(LogInDTO loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName); 
            if(user == null) 
            {
                return Unauthorized("Invalid Username or Password!");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Username or Password!");
                }
            }
            user.DateLastSignedIn = DateTime.Now;
            return Ok(user);
        }

        private async Task<bool> UserNameExists(string userName) 
        { 
            return await _context.Users.AnyAsync(u => u.UserName == userName);  
        }
        private async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.UserEmail == email);
        }
    }
}
