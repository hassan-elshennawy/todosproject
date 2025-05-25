using todosproject.Helpers;
using todosproject.Entities;
using todosproject.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace todosproject.DAL
{
    public class UseresDAL
    {
        private readonly AppDbContext _context;

        public UseresDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateUser(CreateUserRequestDTO dto)
        {
            User newUser = new User();

            newUser.Name = dto.Name;
            newUser.email = dto.email;
            newUser.age = dto.age;
            newUser.password = dto.password;
            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception("error saving to database :" + ex);
            }
            return "Created Successfully";
           
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower());
        }


    }
}
