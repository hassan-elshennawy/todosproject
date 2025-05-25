using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todosproject.DSL;
using todosproject.Entities.DTOs;
using todosproject.Entities.Enums;
using todosproject.Helpers;

namespace firstDotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsersDSL _users;
        public AuthController(UsersDSL users)
        {
            _users = users;
        }
        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateUser(CreateUserRequestDTO userDTO)
        {
            return await _users.CreateUser(userDTO);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> SignIn(LoginDTO loginDTO)
        {
            return await _users.Login(loginDTO);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize]
        [RoleAuth(UserRole.Admin)]
        public async Task<ActionResult<string>> adminPage()
        {
            return "you are admin";
        }

        [HttpGet]
        [Route("user")]
        [Authorize]
        [RoleAuth(UserRole.NUser)]

        public async Task<ActionResult<string>> userPage()
        {
            return "you are admin";
        }


    }
}
