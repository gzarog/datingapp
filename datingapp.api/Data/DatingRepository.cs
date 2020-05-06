using System.Collections.Generic;
using System.Threading.Tasks;
using datingapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T :class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T :class
        {
             _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(f=>f.UserID == userId && f.IsMain);
            return photo;
            
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await  _context.Photos.FirstOrDefaultAsync(photo=>photo.ID == id);
            return photo;
        }

        public async Task<User>  GetUser(int ID)
        {
           var user = await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.ID == ID);
           return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
           var users = await _context.Users.Include(p=>p.Photos).ToListAsync();
           return users;

        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; 
        }
    }
}