using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterServices _registerServices;
        public RegisterController(IRegisterServices register)
        {
            _registerServices = register;
        }
        [HttpPost]
        public async Task<ActionResult<object>> Register(User user)
        {
            return await _registerServices.Register(user);
        }
    }
}
