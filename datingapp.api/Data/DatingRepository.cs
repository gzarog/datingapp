using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingapp.api.Helpers;
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
           var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == ID);
           return user;
        }

        public async Task<PageList<User>> GetUsers( UserParams userParams)
        {
           var users =  _context.Users.OrderByDescending(o =>o.LastActive).AsQueryable();
            users = users.Where(u=> u.Id != userParams.UserId);
            users = users.Where(u=> u.Gender == userParams.Gender);

            if (userParams.MinAge !=18 || userParams.MaxAge !=99)
            {
                var minDateOfBirth = DateTime.Today.AddYears(-userParams.MaxAge - 1 );
                var maxDateOfBirth = DateTime.Today.AddYears(-userParams.MinAge );
                users = users.Where(u => u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);
            }
            
           if(!string.IsNullOrEmpty(userParams.OrderBy)){
               switch (userParams.OrderBy)
               {
                   case "created":
                    users = users.OrderByDescending(o =>o.Created);
                    break;
                    default:
                     users = users.OrderByDescending(o =>o.LastActive);
                     break;


               }
           }
           
           return await PageList<User>.CreateAsync(users , userParams.PageNumber, userParams.PageSize);

        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; 
        }
    }
}