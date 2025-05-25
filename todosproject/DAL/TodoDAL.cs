using Microsoft.EntityFrameworkCore;
using todosproject.Entities;
using todosproject.Entities.DTOs;
using todosproject.Helpers;

namespace todosproject.DAL
{
    public class TodoDAL
    {
        private readonly AppDbContext _context;

        public TodoDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> CreateTodo(CreateTodoRequest request,int userId)
        {
            var todo = new Todo
            {
                Title = request.Title,
                Description = request.Description,
                UserId = userId
            };
            await _context.Todos
                .AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> GetById(int id)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentNullException();
            return todo;
        }

        public async Task<List<Todo>> GetAllTodos(int Id)
        {
           var todos = await _context.Todos
                .Where(t => t.Id == Id)
                .ToListAsync();
            return todos;
        }

        public async Task<List<Todo>> GetNotCompletedTodos(int Id)
        {
            var todos = await _context.Todos
                .Where(t => t.Id == Id && t.IsDone == false)
                .ToListAsync();
            return todos;
        }

        public async Task<List<Todo>> GetCompletedTodos(int Id)
        {
            var todos = await _context.Todos
                .Where(t => t.Id == Id && t.IsDone == true)
                .ToListAsync();
            return todos;
        }

        public async Task DeleteTodo(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException();
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

    }
}
