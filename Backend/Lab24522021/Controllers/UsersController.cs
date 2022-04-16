using Laborator54522021.Models;
using Laborator54522021.Models.DTOs;
using Licenta.Services.AuthService;
using Licenta.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        //post pt autentificare
        public IActionResult Authenticate(UserRequestDTO user)
        {

            var response = _userService.Authentificate(user);
            if(response== null)
            {
                return BadRequest(new { Message = "Username or Password is invalid" });
            }
            return Ok(response);
        }

        //create -http post
        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult Create (UserRequestDTO user)
        {

            //User.AddIdentity(UserToCreate.);
            try { 
            var result=_userService.Create(user);
            return Ok(new { token = result, isSuccess = true });
        }
            catch(Exception e)
            {
                return Ok(new { token = "", isSuccess = false });
            }
        }


        //Trebuie facut si un CREATE admin (vezi lab 5 min 59

       // [Authorize(Roles ="Admin")] // doar cei care au  drepturi de admin, pot accesa acest end point
       // [Authorization(Role.Admin)]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var user = _userService.GetAllUsers();
            return Ok(user);
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var user = _userService.GetById(id);
            var result =  _userService.Delete(user.Id);// nu merge delete 

           
            
            return Ok();
        }


    }

}
    
