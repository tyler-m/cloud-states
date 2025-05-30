﻿using Microsoft.EntityFrameworkCore;
using CloudStates.API.Data;
using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    internal class UserRepository(CloudStatesDbContext _db) : IUserRepository
    {
        public async Task<User?> AddAsync(User user)
        {
            await _db.Users.AddAsync(user);

            if (await _db.SaveChangesAsync() > 0)
            {
                return user;
            }

            return null;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
