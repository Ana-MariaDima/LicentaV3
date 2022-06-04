using Laborator54522021.Models;
using Laborator54522021.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.AuthService
{
    public interface IUserService
    {
        //Auth
        UserResponseDTO Authentificate(UserRequestDTO model);

        //GetAll
        IEnumerable<User> GetAllUsers();
        //GetById


        User GetById(Guid id);
        string Create(UserRequestDTO model);

        string Update(User model);


        bool Delete(Guid id);
    }
}
