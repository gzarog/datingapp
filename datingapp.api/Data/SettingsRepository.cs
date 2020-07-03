using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingapp.api.Helpers;
using datingapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Data
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly DataContext _context;
        public SettingsRepository(DataContext context)
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

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; 
        }

        public async Task<List<EmailTemplate>> GetEmailTemplatesAsync()
        {
            return await _context.EmailTemplates.ToListAsync(); 
        }
    }

}