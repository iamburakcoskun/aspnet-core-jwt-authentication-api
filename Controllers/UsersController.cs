using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtCoreDemo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace jwtCoreDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            // throw new Exception("No entered!");

            var user = _userService.Authenticate(userParam.KullaniciAdi, userParam.Sifre);

            if (user == null)
                return BadRequest(new { message = "Kullanici veya şifre hatalı!" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            bool isUserExist;
            isUserExist = _userService.IsUserExist(user);

            if (isUserExist)
                return BadRequest("Kullanıcı adı sistemde kayıtlıdır.");

            var users = _userService.Insert(user);
            return Ok(users);
        }
    }
}