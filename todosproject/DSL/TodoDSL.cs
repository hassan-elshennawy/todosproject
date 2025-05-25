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
        private readonly IHttpContextAccessor _httpContext;

        public TodoDSL(TodoDAL todoDAL, IHttpContextAccessor httpContext)
        {
            _todoDAL = todoDAL;
            _httpContext = httpContext;
        }

        private async Task Validate(int id)
        {
            var userClaim = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) { throw new NullReferenceException(); }
            int userId = int.Parse(userClaim.Value);
            var todo = await _todoDAL.GetById(id);
            if (userId == 0 && userId != todo.UserId) { throw new UnauthorizedAccessException(); }
        }

        private async Task<int> ValidateAndGetUserId()
        {
            var userClaim = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) { throw new NullReferenceException(); }
            int userId = int.Parse(userClaim.Value);
            //var todo = await _todoDAL.GetById(id);
            if (userId == 0) { throw new UnauthorizedAccessException(); }
            return userId;
        }

        public async Task<Todo> CreateTodo(CreateTodoRequest request)
        {
            var userIdClaim = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) 
            {
                throw new InvalidOperationException();
            }
            int userId = int.Parse(userIdClaim.Value);
            return await _todoDAL.CreateTodo(request, userId);
        }

        public async Task DeleteTodo(int id)
        {
            //await Validate();
            var userClaim = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) { throw new NullReferenceException(); }
            int userId = int.Parse(userClaim.Value);
            var todo = await _todoDAL.GetById(userId);
            if(userId == 0 && userId != todo.UserId) { throw new UnauthorizedAccessException(); }

            await _todoDAL.DeleteTodo(todo);
        }
        public async Task UpdateTodo(int id,UpdateTodoDto updateTodoDto)
        {
            //await Validate();
            var userClaim = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) { throw new NullReferenceException(); }
            int userId = int.Parse(userClaim.Value);
            var todo = await _todoDAL.GetById(id);
            if (userId == 0 && userId != todo.UserId) { throw new UnauthorizedAccessException(); }

            if(updateTodoDto.Title != null) { todo.Title = updateTodoDto.Title; }
            if(updateTodoDto.Description != null) { todo.Description = updateTodoDto.Description; }
            if(updateTodoDto.isDone != null) { todo.IsDone = updateTodoDto.isDone.Value; }

            await _todoDAL.UpdateTodo(todo);
        }

        public async Task<Todo> GetTodo(int id)
        {
            await Validate(id);
            return await _todoDAL.GetById(id);
        }

        public async Task<List<Todo>> GetAllTodos() 
        {
            int userId = ValidateAndGetUserId().Result;
            return await _todoDAL.GetAllTodos(userId);
        }

        public async Task<List<Todo>> GetCompletedTodos()
        {
            int userId = ValidateAndGetUserId().Result;
            return await _todoDAL.GetCompletedTodos(userId);
        }

        public async Task<List<Todo>> GetPendingTodos()
        {
            int userId = ValidateAndGetUserId().Result;
            return await _todoDAL.GetPendingTodos(userId);
        }
    }
}
