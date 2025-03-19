using Dapper;
using Identity.Application.DTO;
using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using Identity.Application.Wrappers;
using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Domain.Shares;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.Shares;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> ChangePassword(Guid userId, string newHashPassword)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return false;

            user.PasswordHash = newHashPassword;

            return true;
        }

        public async Task<bool> ConfirmEmail(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return false;

            user.IsEmailConfirmed = true;

            return true;
        }

        public async Task<bool> IsVerifyCode(Guid userId, string code)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return false;

            var confirm = await _context.EmailConfirmationTokens
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Token.Equals(code) && e.ExpirationDate >= DateUtility.GetCurrentDateTime());

            return confirm != null;
        }

        public async Task<string> GenerateCodeConfirmEmail(Guid userId)
        {
            string code = Util.Generate6DigitCode();

            await _context.EmailConfirmationTokens.AddAsync(new EmailConfirmationToken
            {
                Id = Guid.NewGuid(),
                ExpirationDate = DateUtility.GetCurrentDateTime().AddMinutes(5),
                Token = code,
                UserId = userId
            });

            return code;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> UpdateUser(User u)
        {
            var user = await _context.Users.FindAsync(u.Id);

            if (user == null) return false;

            user.UserName = u.UserName;
            user.Email = u.Email;

            return true;
        }

        public async Task<bool> SaveRefreshToken(Guid userId, string refreshToken, DateTime ExpirationDate)
        {
            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshToken,
                ExpirationDate = ExpirationDate,
                UserId = userId
            });

            return true;
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(t => t.User)
                .ThenInclude(t => t.UserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(t => t.Token.ToLower().Equals(refreshToken.ToLower()) 
                && t.ExpirationDate >= DateUtility.GetCurrentDateTime());

            return token?.User;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(user => user.UserName.ToLower().Equals(username.ToLower()));
        }

        public async Task<bool> ChangeIsDeletedUser(Guid userId, bool isDeleted)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return false;

            user.IsActive = !isDeleted;

            return true;
        }

    }
}
