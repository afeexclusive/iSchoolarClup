using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iScholar.Data;
using iScholar.Models;
using iScholar.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace iScholar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IOptions<ApplicationSettings> appSettings;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            ApplicationDbContext context, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.appSettings = appSettings;
        }

        [HttpPost]
        [Route("register")]
        //[AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                //Save Users to appropriate table
                if (result.Succeeded)
                {
                    CreateUserToTable(model);
                }
               
                return Ok(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CreateUserToTable(RegisterViewModel model)
        {

            if (model.RegisterAs == UserType.School)
            {
                School school = new School() 
                { 
                    SchoolId = Guid.NewGuid(),
                    Email = model.Email,
                    Name = model.SchoolName,
                    Phone = model.PhoneNumber,
                    State = model.State
                };
                context.Schools.Add(school);
                context.SaveChanges();
            }
            else if (model.RegisterAs == UserType.Teacher)
            {
                Teacher teacher = new Teacher()
                { 
                    Email = model.Email,
                    Name = model.Name,
                    Phone = model.PhoneNumber,
                    School = new School { Name = model.SchoolName}
                };
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
            else
            {
                Student student = new Student()
                { 
                    Email = model.Email,
                    Name = model.Name,
                    ParentPhone = model.PhoneNumber,
                    School = new School { Name = model.SchoolName }
                };
                context.Students.Add(student);
                context.SaveChanges();
            }
       
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Email or Password is incorrect" });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message= "Logout Successful"});
        }
    }
}
