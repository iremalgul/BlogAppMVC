using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppMVC.Context.Entities;
using BlogAppMVC.DTOs;
using BlogAppMVC.Models;
using BlogAppMVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.UnitOfWork;
using System;

namespace BlogAppMVC.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IMapper Mapper
        {
            get;
        }

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<int> CreateUser(UserDTO UserDto)
        {
            try
            {
                var user = Mapper.Map<User>(UserDto);

                // Şifre hashlemesi
                var hasher = new PasswordHasher<User>();

                user.Password = hasher.HashPassword(user, UserDto.Password);

                await _unitOfWork.User.Insert(user);
                return user.UserId;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var query = _unitOfWork.User.Table;
            var users = await query.ProjectTo<UserDTO>(Mapper.ConfigurationProvider).ToListAsync();
            return users;
        }

        public async Task<bool> IsUser(LoginViewModel model)
        {
            var user = await _unitOfWork.User.Table
          .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            return user != null;
        }

        public async Task<UserDTO> GetUser(LoginViewModel model)
        {
            var user = await _unitOfWork.User.Table
        .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return null;
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return Mapper.Map<UserDTO>(user);
            }

            return null;
        }

        public async Task<UserDTO> GetUserProfile(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var user = await _unitOfWork.User.Table
                .Include(x => x.Posts)
                .Include(x => x.Comments)
                    .ThenInclude(x => x.Post)
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                return null;

            return Mapper.Map<UserDTO>(user);
        }
    }
}
