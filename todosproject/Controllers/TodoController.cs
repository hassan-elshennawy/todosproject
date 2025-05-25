using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using todosproject.DSL;
using todosproject.Entities;
using todosproject.Entities.DTOs;
using todosproject.Entities.Enums;
using todosproject.Helpers;

namespace firstDotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDSL _todoDSL;

        public TodoController(TodoDSL todoDSL)
        {
            _todoDSL = todoDSL;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<Todo>> CreateTodo(CreateTodoRequest request)
        {
            return await _todoDSL.CreateTodo(request);
        }

        [HttpPost("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _todoDSL.DeleteTodo(id);
            return Ok("Deleted successfully");
        }

        [HttpPatch("update/{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateTodo(int id, [FromBody]UpdateTodoDto dto) 
        {
            await _todoDSL.UpdateTodo(id, dto);
            return Ok("edited successfully");
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            return await _todoDSL.GetTodo(id);
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<List<Todo>>> getAllTodos()
        {
            return await _todoDSL.GetAllTodos();
        }

        [HttpGet("iscompleted")]
        public async Task<ActionResult<List<Todo>>> getCompletedTodos()
        {
            return await _todoDSL.GetCompletedTodos();
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<Todo>>> getPendingTodos()
        {
            return await _todoDSL.GetPendingTodos();
        }



    }
}
