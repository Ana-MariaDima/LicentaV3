using Laborator54522021.Models;
using Laborator54522021.Models.DTOs;
using Licenta.Data;
using Licenta.Utilities.JWTutils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Licenta.Services.AuthService
{
    public class UserService : IUserService
    {
        public Context context;
        public IJWTUtils iJWTUtils; 

        public UserService(Context _context, IJWTUtils _iJWTUtils)
        {
            context = _context;
            iJWTUtils = _iJWTUtils;
        }

        public UserResponseDTO Authentificate(UserRequestDTO model)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == model.Username);
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                return null;
            }
            //JWT generation (Json Web Token)
            var jwtToken = iJWTUtils.GenerateJWTToken(user);
            return new UserResponseDTO(user, jwtToken);
        }

        public  string Create (UserRequestDTO model)
        {

            var UserToCreate = new User
            {
                FirstName = model.FirstName,
                LastName = model.LasttName,
                Username = model.Username,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = Role.User
            };

            /*var user = context.Users.FirstOrDefault(x => x.Username == model.Username);
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                return true;
            }*/
            //JWT generation (Json Web Token)
            context.Users.Add(UserToCreate);
            context.SaveChanges();
            var jwtToken = iJWTUtils.GenerateJWTToken(UserToCreate);
            return jwtToken;
        }

        public IEnumerable<User> GetAllUsers()
        {
            // var users = context.Users;
            // return users;
            // return
            return context.Users.Where(x => true);
           //return context.Users;
        }

        public User GetById(Guid id)
        {
            return context.Users.Where(x =>x.Id==id).FirstOrDefault();
        }

        public bool Delete(Guid id)
        {
            var user= context.Users.Where(x => x.Id == id).FirstOrDefault();
            var user2 = context.Users.Remove(user);
            return true;
        }

    }
}
