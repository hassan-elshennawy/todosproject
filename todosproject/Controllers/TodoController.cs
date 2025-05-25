using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todosproject.DSL;
using todosproject.Entities;
using todosproject.Entities.DTOs;
using todosproject.Entities.Enums;
using todosproject.Helpers;

namespace firstDotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly TodoDSL _todoDSL;

        public TodoController(TodoDSL todoDSL)
        {
            _todoDSL = todoDSL;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Todo>> CreateTodo(CreateTodoRequest request)
        {
            return await _todoDSL.CreateTodo(request);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _todoDSL.DeleteTodo(id);
            return Ok("Deleted successfully");
        }



    }
}
