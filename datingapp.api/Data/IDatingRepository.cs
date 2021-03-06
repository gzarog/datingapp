using System.Collections.Generic;
using System.Threading.Tasks;
using datingapp.api.Helpers;
using datingapp.api.Models;

namespace datingapp.api.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        Task<bool> SaveAll();
        Task<PageList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int ID);
        Task<Photo> GetPhoto(int id);

        Task<Photo> GetMainPhotoForUser(int userId);


    }
}