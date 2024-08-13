using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Resource;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService=userService;
        }

        [HttpGet]
        public async Task<List<User>> GetAllAsync() =>
          await _userService.GetAllAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResource>> Post(Login login)
        {
            return  await _userService.Login(login.Email, login.Password);
        }

        [HttpPost]
        public async Task<ActionResult<LoginResource>> Post(User newsUser)
        {
             return await _userService.Register(newsUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updateUser)
        {
            var student = await _userService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            updateUser.Id = student.Id;

            await  _userService.UpdateProfile(id, updateUser);

            return Ok(await _userService.GetAsync(id));

        }
    }
}
