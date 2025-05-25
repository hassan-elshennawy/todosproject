using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using todosproject.DAL;
using todosproject.Entities;
using todosproject.Entities.DTOs;

namespace todosproject.DSL
{
    public class TodoDSL
    {
        private readonly TodoDAL _todoDAL;
        private readonly HttpContext _httpContext;

        public TodoDSL(TodoDAL todoDAL, HttpContext httpContext)
        {
            _todoDAL = todoDAL;
            _httpContext = httpContext;
        }

        public async Task<Todo> CreateTodo(CreateTodoRequest request)
        {
            var userIdClaim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) 
            {
                throw new InvalidOperationException();
            }
            int userId = int.Parse(userIdClaim.Value);
            return await _todoDAL.CreateTodo(request, userId);
        }

        public async Task DeleteTodo(int id)
        {
            var userClaim = _httpContext.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) { throw new NullReferenceException(); }
            int userId = int.Parse(userClaim.Value);
            var todo = await _todoDAL.GetById(userId);
            if(userId == 0 && userId != todo.UserId) { throw new UnauthorizedAccessException(); }
            await _todoDAL.DeleteTodo(todo);
        }
    }
}
