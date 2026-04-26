using System;
using Test2.models.Entities;
using Test2.models.Repositories;
using Test2.models.Utilities;
using Test2.models.ViewModels;

namespace Test2.models.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepo;

        public AuthService(UserRepository repository)
        {
            _userRepo = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Register(RegisterViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (_userRepo.UsernameExists(model.Username))
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = model.Username,
                PasswordHash = PasswordHelper.Hash(model.Password)
            };

            _userRepo.Add(user);
        }

        public bool Login(LoginViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var user = _userRepo.GetByUsername(model.Username);

            if (user == null)
                return false;

            return PasswordHelper.Verify(model.Password, user.PasswordHash);
        }
    }
}