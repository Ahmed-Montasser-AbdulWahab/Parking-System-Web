using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking_System.DbContext;
using Parking_System.Model;

namespace Parking_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private readonly IAppDbContext appDbContext;

        public SystemUserController(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        [HttpPost("login"), AllowAnonymous]
        public IActionResult Login([FromForm] AuthenticationRequest authenticationRequest)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager();
            var authResult = jwtAuthenticationManager.AuthenticateAdminAndOpertor(authenticationRequest.Email, authenticationRequest.Password);
            if (authResult == null)
                return Unauthorized();
            else
                return Ok(authResult);
        }

        
        [HttpPost("signup"), Authorize(Roles = "admin")]
        public IActionResult Signup([FromBody]SystemUser systemUser) {

            bool systemUserType = false;

            if (string.IsNullOrEmpty(systemUser.Type) || string.IsNullOrEmpty(systemUser.Email) || string.IsNullOrEmpty(systemUser.Name) || string.IsNullOrEmpty(systemUser.Password)) {

                return BadRequest("Check Data Provided");
            }

            string type = systemUser.Type.ToLower();
            if ( type == "admin" || type == "a")
            {
                systemUserType = true;
            } 
            else if (type == "operator" || type == "o")
            {
                systemUserType = false;
            }
            else {
                return BadRequest("Check Role Provided \'Admin\' Or \'Operator\' ");
            }
            var IsSignedUp = appDbContext.AddSystemUser(systemUser.Email, systemUser.Password, systemUser.Name, systemUserType);

            if (IsSignedUp)
            {
                return Created("",$"{systemUser.Email} is created");
            }
            return BadRequest("Check Provided Data, i.e:Data may be duplicated");
        }

        [HttpGet("{category?}"), Authorize(Roles = "admin")]
        public IActionResult GetSystemUsers(string category)
        {

        }

    }
}
