﻿using Introduce.Data.Models;
using Introduce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Contexts
{
    public class SeedData
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _hasher;

        public SeedData(AppDbContext context,
                        IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public void PlantData()
        {
            const string userId = "adminUser";
            const string password = "123123";

            if (!_context.Users.Any())
            {
                var hashedPasswordInfo = _hasher.SetHashedPassword(userId, password);

                var user = new User
                {
                    UserId = userId,
                    UserEmail = "admin@admin.com",
                    UserName = "운영자",
                    RngSalt = hashedPasswordInfo.RngSalt,
                    HashedPassword = hashedPasswordInfo.HashedPassword,
                    JoinedDate = DateTime.Now,
                };

                _context.Users.Add(user);   
            }

            if (!_context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        RoleId = "SystemUser",
                        RoleName = "시스템 사용자",
                        RolePriority = 0
                    },
                    new Role
                    {
                        RoleId = "GeneralUser",
                        RoleName = "일반 사용자",
                        RolePriority = 1
                    },
                };

                _context.Roles.AddRange(roles);
            }

            if (!_context.RoleByUsers.Any())
            {
                var roleByUsers = new RoleByUser
                {
                    UserId = userId,
                    RoleId = "SystemUser",
                };

                _context.RoleByUsers.Add(roleByUsers);
            }

            _context.SaveChanges();
        }
    }
}
