/*
  TODO:
    1. E-mail confirmation - with confirmation token
    2. Reset Password Request - send request password e-mail (reset password token)
    3. Validate Token - don't remember why
    4. Reset Password - valid reset password token + new password == new password set
*/
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Webapi.Models;
using Webapi.RequestModels;
using Webapi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Webapi.Controllers
{
  [Route("api/v1/application_users")]
  [ApiController]
  public class ApplicationUsersController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IConfiguration config;
    private readonly TokenGenerator tokenGenerator;
    private readonly AppDbContext dbContext;

    public ApplicationUsersController(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager,
      IConfiguration config,
      AppDbContext dbContext,
      TokenGenerator tokenGenerator)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.config = config;
      this.tokenGenerator = tokenGenerator;
      this.dbContext = dbContext;
    }

    /*
      POST (Guest) /api/v1/application_users
    */
    [HttpPost]
    public async Task<ActionResult> SignUp([FromBody] ApplicationUserRequest requestBody)
    {
      if (!ModelState.IsValid)
        return BadRequest(new { errors = new { global = "Request body is invalid" } });

      var testEmailUnique = await this.userManager.FindByEmailAsync(requestBody.Email);
      if (testEmailUnique != null)
        return BadRequest(new { errors = new { global = "This e-mail is already taken" } });

      var applicationUser = new ApplicationUser
      {
        UserName = requestBody.UserName,
        Email = requestBody.Email,
        FullName = requestBody.FullName
      };

      try
      {
        var result = await this.userManager.CreateAsync(applicationUser, requestBody.Password);

        // Manually added Roles in DB = "1 - Admin" & "2 - Customer"
        if (requestBody.AdminKey != null && requestBody.AdminKey == this.config["Admin:Key"])
          await this.userManager.AddToRoleAsync(applicationUser, Roles.Admin);
        else
          await this.userManager.AddToRoleAsync(applicationUser, Roles.Customer);

        if (result.Succeeded)
          return Ok(new { email = requestBody.Email });
        else
        {
          var error = result.Errors.ToList().First();
          var code = error.Code;
          var desc = error.Description;
          return BadRequest(new { errors = new { global = code + ": " + desc } });
        }
      }
      catch (Exception ex) { throw ex; }
    }

    /*
      POST (Guest) /api/v1/application_users/list
    */
    [HttpGet]
    [Authorize(Roles=Roles.Admin)]
    public async Task<ActionResult> GetAll()
    {
      return Ok(dbContext.Users.ToList());
    }

    /*
      POST (Guest) /api/v1/application_users/signin
    */
    [HttpPost("signin")]
    public async Task<ActionResult> SignIn([FromBody] SignInRequest requestBody)
    {
      if (!ModelState.IsValid)
        return BadRequest(new { errors = new { global = "Request body is invalid" } });

      var user = await this.userManager.FindByEmailAsync(requestBody.Email);

      if (user == null || await this.userManager.CheckPasswordAsync(user, requestBody.Password) == false)
        return BadRequest(new { errors = new { global = "E-mail not found or password is incorrect" } });

      var role = await this.userManager.GetRolesAsync(user);
      var userRole = role.FirstOrDefault() ?? "Customer";
      var token = this.tokenGenerator.GenerateAuthenticationToken(user.Id, userRole, requestBody.Email, false);

      return Ok(new { token = token, email = requestBody.Email, userName = user.UserName, isAdmin = userRole == Roles.Admin, isEmailConfirmed = false });
    }

    /*
      GET (Guest) /api/v1/application_users/{email}
    */
    [HttpGet("{email}")]
    [Authorize(Roles=Roles.Admin)]
    public async Task<ActionResult> GetUserByEmail([FromRoute] string email)
    {
      if (email == "")
        return BadRequest(new { errors = new { global = "You must inform a user email to get the profile" } });

      var applicationUser = await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);

      if (applicationUser != null)
        return Ok(new
        {
          userName = applicationUser.UserName,
          fullName = applicationUser.FullName,
          email = applicationUser.Email,
          isEmailConfirmed = applicationUser.EmailConfirmed,
        });
      else
        return BadRequest(new { errors = new { global = "User not found with the passed email" } });
    }
  }
}
